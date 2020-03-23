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
