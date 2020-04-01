-- Practice Transactions
USE [A01-School]
GO

/*
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'SprocName')
    DROP PROCEDURE SprocName
GO

CREATE PROCEDURE SprocName
    -- Parameters here
AS
    -- Body of procedure here
RETURN
GO
*/

-- 1. Create a stored procedure called DissolveClub that will accept a club id as its parameter. Ensure that the club exists before attempting to dissolve the club. You are to dissolve the club by first removing all the members of the club and then removing the club itself.
--      - Delete of rows in the Activity table
--      - Delete of rows in the Club table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'DissolveClub')
    DROP PROCEDURE DissolveClub
GO
CREATE PROCEDURE DissolveClub
    -- Parameters here
    @ClubId     varchar(10)
AS
    -- Validatation:
    -- A) Make sure the ClubId is not null
    IF @ClubId IS NULL
    BEGIN
        RAISERROR('ClubId is required', 16, 1)
    END
    ELSE
    BEGIN
        -- B) Make sure the Club exists
        IF NOT EXISTS(SELECT ClubId FROM Club WHERE ClubId = @ClubId)
        BEGIN
            RAISERROR('That club does not exist', 16, 1)
        END
        ELSE
        BEGIN
            -- Transaction:
            BEGIN TRANSACTION -- Starts the transaction - everything is temporary
            -- 1) Remove members of the club (from Activity)
            DELETE FROM Activity WHERE ClubId = @ClubId
            -- Remember to do a check of your global variables to see if there was a problem
            IF @@ERROR <> 0 -- then there's a problem with the delete, no need to check @@ROWCOUNT
            BEGIN
                ROLLBACK TRANSACTION -- Ending/undoing any temporary DML statements
                RAISERROR('Unable to remove members from the club', 16, 1)
            END
            ELSE
            BEGIN
                -- 2) Remove the club
                DELETE FROM Club WHERE ClubId = @ClubId
                IF @@ERROR <> 0 OR @@ROWCOUNT = 0 -- there's a problem
                BEGIN
                    ROLLBACK TRANSACTION
                    RAISERROR('Unable to delete the club', 16, 1)
                END
                ELSE
                BEGIN
                    COMMIT TRANSACTION -- Finalize all the temporary DML statement
                END
            END
        END
    END
RETURN
GO

-- Test my stored procedure
-- SELECT * FROM Club
-- SELECT * FROM Activity
EXEC DissolveClub 'CSS'
EXEC DissolveClub 'NASA1'
EXEC DissolveClub 'WHA?'

-- 2. In response to recommendations in our business practices, we are required to create an audit record of all changes to the Payment table. As such, all updates and deletes from the payment table will have to be performed through stored procedures rather than direct table access. For these stored procedures, you will need to use the following PaymentHistory table.
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PaymentHistory')
    DROP TABLE PaymentHistory

CREATE TABLE PaymentHistory
(
    AuditID         int
        CONSTRAINT PK_PaymentHistory
        PRIMARY KEY
        IDENTITY(10000,1)
                                NOT NULL,
    PaymentID       int         NOT NULL,
    PaymentDate     datetime    NOT NULL,
    PriorAmount     money       NOT NULL,
    PaymentTypeID   tinyint     NOT NULL,
    StudentID       int         NOT NULL,
    DMLAction       char(6)     NOT NULL
        CONSTRAINT CK_PaymentHistory_DMLAction
            CHECK  (DMLAction IN ('UPDATE', 'DELETE'))
)
GO

-- 2.a. Create a stored procedure called UpdatePayment that has a parameter to match each column in the Payment table. This stored procedure must first record the specified payment's data in the PaymentHistory before applying the update to the Payment table itself.
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'UpdatePayment')
    DROP PROCEDURE UpdatePayment
GO

CREATE PROCEDURE UpdatePayment
    @PaymentID      int,
    @PaymentDate    datetime,
    @Amount         money,
    @PaymentTypeID  int,
    @StudentID      int
AS
    IF @PaymentID IS NULL OR @PaymentDate IS NULL OR @Amount IS NULL OR @PaymentTypeID IS NULL OR @StudentID IS NULL
    BEGIN
        RAISERROR('All parameters are required', 16, 1)
    END
    ELSE
    BEGIN
        BEGIN TRANSACTION
        INSERT INTO PaymentHistory(PaymentID, PaymentDate, PriorAmount, PaymentTypeID, StudentID, DMLAction)
        SELECT PaymentID, PaymentDate, Amount, PaymentTypeID, StudentID, 'UPDATE'
        FROM   Payment
        WHERE  PaymentID = @PaymentID
        IF @@ERROR <> 0 OR @@ROWCOUNT = 0
        BEGIN
            RAISERROR('Unable to create audit record - aborting payment update', 16, 1)
            ROLLBACK TRANSACTION
        END
        ELSE
        BEGIN
            UPDATE  Payment
            SET     PaymentDate = @PaymentDate,
                    Amount = @Amount,
                    PaymentTypeID = @PaymentTypeID,
                    StudentID = @StudentID
            WHERE   PaymentID = @PaymentID
            IF @@ERROR <> 0 OR @@ROWCOUNT = 0
            BEGIN
                RAISERROR('Unable to update payment record', 16, 1)
                ROLLBACK TRANSACTION
            END
            ELSE
            BEGIN
                COMMIT TRANSACTION
            END
        END
    END
RETURN
GO

-- 2.b. Create a stored procedure called DeletePayment that has a parameter identifying the payment ID and the student ID. This stored procedure must first record the specified payment's data in the PaymentHistory before removing the payment from the Payment table.
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'DeletePayment')
    DROP PROCEDURE DeletePayment
GO

CREATE PROCEDURE DeletePayment
    @PaymentID      int,
    @StudentID      int
AS
    IF @PaymentID IS NULL OR @StudentID IS NULL
    BEGIN
        RAISERROR('All parameters are required', 16, 1)
    END
    ELSE
    BEGIN
        BEGIN TRANSACTION
        INSERT INTO PaymentHistory(PaymentID, PaymentDate, PriorAmount, PaymentTypeID, StudentID, DMLAction)
        SELECT PaymentID, PaymentDate, Amount, PaymentTypeID, StudentID, 'DELETE'
        FROM   Payment
        WHERE  PaymentID = @PaymentID
        IF @@ERROR <> 0 OR @@ROWCOUNT = 0
        BEGIN
            RAISERROR('Unable to create audit record - aborting payment deletion', 16, 1)
            ROLLBACK TRANSACTION
        END
        ELSE
        BEGIN
            DELETE  Payment
            WHERE   PaymentID = @PaymentID
            IF @@ERROR <> 0 OR @@ROWCOUNT = 0
            BEGIN
                RAISERROR('Unable to remove payment record', 16, 1)
                ROLLBACK TRANSACTION
            END
            ELSE
            BEGIN
                COMMIT TRANSACTION
            END
        END
    END
RETURN
GO

-- 3. Create a stored procedure called ArchivePayments. This stored procedure must transfer all payment records to the StudentPaymentArchive table. After archiving, delete the payment records.
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'StudentPaymentArchive')
    DROP TABLE StudentPaymentArchive

CREATE TABLE StudentPaymentArchive
(
    ArchiveId       int
        CONSTRAINT PK_StudentPaymentArchive
        PRIMARY KEY
        IDENTITY(1,1)
                                NOT NULL,
    StudentID       int         NOT NULL,
    FirstName       varchar(25) NOT NULL,
    LastName        varchar(35) NOT NULL,
    PaymentMethod   varchar(40) NOT NULL,
    Amount          money       NOT NULL,
    PaymentDate     datetime    NOT NULL
)
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'ArchivePayments')
    DROP PROCEDURE ArchivePayments
GO

CREATE PROCEDURE ArchivePayments
AS
    BEGIN TRANSACTION
    INSERT INTO StudentPaymentArchive(StudentID, FirstName, LastName, PaymentMethod, Amount, PaymentDate)
    SELECT S.StudentID, FirstName, LastName, PaymentTypeDescription, Amount, PaymentDate
    FROM   Student AS S
        INNER JOIN Payment AS P ON S.StudentID = P.StudentID
        INNER JOIN PayemtnType AS PT ON P.PaymentTypeID = PT.PaymentTypeID
    IF @@ERROR <> 0 OR @@ROWCOUNT = 0
    BEGIN
        RAISERROR('Unable to create archive records', 16, 1)
        ROLLBACK TRANSACTION
    END
    ELSE
    BEGIN
        DELETE  Payment
        IF @@ERROR <> 0 OR @@ROWCOUNT = 0
        BEGIN
            RAISERROR('Unable to remove all payment records', 16, 1)
            ROLLBACK TRANSACTION
        END
        ELSE
        BEGIN
            COMMIT TRANSACTION
        END
    END
RETURN
GO
