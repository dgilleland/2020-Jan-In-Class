-- Triggers Samples
USE [A01-School]
GO

/*
If you recall about Transactions, they are used whenever we have 2 or more of an INSERT/UPDATE/DELETE.
A transaction holds the database changes in a temporary state and finalizes it with a COMMIT TRANSACTION.
For individual INSERT/UPDATE/DELETE statements, SQL Server manages its own transaction for that change
on the database table. SQL Server does this using two temporary tables it constructs for the DML statement.
These temporary tables have the same columns that the table being affected has. These tables have special names:
- deleted
- inserted

Triggers are our opportunity to interecept the internal transaction that SQL server does for each INSERT/UPDATE/DELETE.
Triggers are never called directly by our scripts, instead they are called (or "triggered") by SQL server
as it performs its internal transaction.
Triggers are "attached" to individual tables. If you drop the table, then the trigger is dropped too.

Triggers are used for:
- Performing complex validations that cannot be done with ordinary CHECK constraints or other contraints.
  For example, I might need to compare something in this table with some other set of information from
  multiple tables, or against some aggregates.
- A opportunity to do some archiving or auditing actions
- To prevent an INSERT/UPDATE/DELETE from happening (by doing a ROLLBACK TRANSACTION)
*/
/*
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Table_TriggerType]'))
    DROP TRIGGER Table_TriggerType
GO

CREATE TRIGGER Table_TriggerType
ON TableName
FOR Insert, Update, Delete -- Choose only the DML statement(s) that apply
AS
    -- Body of Trigger
RETURN
GO
*/
-- Making a diagnostic trigger for the first example
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Activity_DML_Diagnostic]'))
    DROP TRIGGER Activity_DML_Diagnostic
GO

CREATE TRIGGER Activity_DML_Diagnostic
ON Activity -- This trigger is "attached" to the Activity table.
-- This trigger will be called anytime there is an INSERT/UPDATE/DELETE
FOR Insert, Update, Delete -- Choose only the DML statement(s) that apply
AS
    -- Body of Trigger
    SELECT 'Activity Table:', StudentID, ClubId FROM Activity ORDER BY StudentID
    SELECT 'Inserted Table:', StudentID, ClubId FROM inserted ORDER BY StudentID
    SELECT 'Deleted Table:', StudentID, ClubId FROM deleted ORDER BY StudentID
RETURN
GO
-- Demonstrate the diagnostic trigger
SELECT * FROM Activity
INSERT INTO Activity(StudentID, ClubId) VALUES (200494476, 'CIPS') -- Our trigger will be called
-- (note: generally, it's not a good idea to change a primary key, even part of one)
UPDATE Activity SET ClubId = 'NASA1' WHERE StudentID = 200494476 -- You can think of an UPDATE as deleting old data and inserting new data in its place.
DELETE FROM Activity WHERE StudentID = 200494476

-- 1. In order to be fair to all students, a student can only belong to a maximum of 3 clubs. Create a trigger to enforce this rule.
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Activity_InsertUpdate]'))
    DROP TRIGGER Activity_InsertUpdate
GO
-- The trigger we want to create is because we need to do some complex validation that can't be done in a regular CHECK constraint.
CREATE TRIGGER Activity_InsertUpdate
ON Activity
FOR Insert, Update -- Choose only the DML statement(s) that apply
AS
    -- Body of Trigger
    IF @@ROWCOUNT > 0 -- It's a good idea to see if any rows were affected first
       AND
       EXISTS (SELECT StudentID FROM Activity
               GROUP BY StudentID HAVING COUNT(StudentID) > 3)
    BEGIN
        -- State why I'm going to abort the changes
        RAISERROR('Max of 3 clubs that a student can belong to', 16, 1)
        -- "Undo" the changes
        ROLLBACK TRANSACTION
    END
RETURN
GO
-- Before doing my tests, examine the data in the table
-- to see what I could use for testing purposes
SELECT * FROM Activity
SELECT StudentID, FirstName, LastName FROM Student WHERE StudentID = 200495500

-- The following test should result in a rollback.
INSERT INTO Activity(StudentID, ClubId)
VALUES (200495500, 'CIPS') -- Robert Smith

SELECT * FROM Activity WHERE StudentID = 200495500

-- Let's look at another student's activity...
SELECT * FROM Activity WHERE StudentID = 200312345

-- The following should succeed
INSERT INTO Activity(StudentID, ClubId)
VALUES (200312345, 'CIPS') -- Mary Jane

SELECT * FROM Activity WHERE StudentID = 200312345

-- Try with an INSERT statement that is trying to do a whole bunch of rows at one time.
-- First, let's check the counts
SELECT StudentID, COUNT(StudentID) FROM Activity WHERE StudentID IN (200122100, 200494476, 200522220, 200978400, 200688700, 200495500) GROUP BY StudentID

INSERT INTO Activity(StudentID, ClubId)
VALUES (200122100, 'CIPS'), -- Peter Codd   -- New to the Activity table
       (200494476, 'CIPS'), -- Joe Cool     -- New to the Activity table
       (200522220, 'CIPS'), -- Joe Petroni  -- New to the Activity table
       (200978400, 'CIPS'), -- Peter Pan    -- New to the Activity table
       (200688700, 'CIPS')  -- Robbie Chan  -- New to the Activity table
      ,(200495500, 'CIPS')  -- Robert Smith -- This would be his 4th club! This is the only row with a "problem"

-- 2. The Education Board is concerned with rising course costs! Create a trigger to ensure that a course cost does not get increased by more than 20% at any one time.
-- We have a complex business rule that we are trying to enforce.
-- How can we tell if a course's cost is being increased by more than 20%? We have to do a comparison between the "before" cost and the "after" cost. We can only do this check in a trigger, if we want the opportunity to do a ROLLBACK.
-- the "before" cost is reflected in the deleted table; the "after" cost is reflected in the inserted table
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Course_Update_CourseCostLimit]'))
    DROP TRIGGER Course_Update_CourseCostLimit
GO

CREATE TRIGGER Course_Update_CourseCostLimit
ON Course -- The inserted and deleted tables will have the same "schema" (columns) as the Course table
FOR Update -- Only doing it on an UPDATE statement, because that's the only time that we have a before/after version of the data
AS
    -- Body of Trigger
    IF @@ROWCOUNT > 0 AND
       EXISTS(SELECT * FROM inserted I INNER JOIN deleted D ON I.CourseId = D.CourseId
              WHERE I.CourseCost > D.CourseCost * 1.20) -- 20% higher
    BEGIN
        RAISERROR('Students can''t afford that much of an increase!', 16, 1)
        ROLLBACK TRANSACTION
    END
RETURN
GO
-- TODO: Write the code that will test this stored procedure.
SELECT * FROM Course
UPDATE Course SET CourseCost = 1000 -- This should fail
UPDATE Course SET CourseCost = CourseCost * 1.21 -- An increase of 21%
UPDATE Course SET CourseCost = CourseCost * 1.195 -- Try an increase of 19.5%
SELECT * FROM Course

-- 3. Too many students owe us money and keep registering for more courses! Create a trigger to ensure that a student cannot register for any more courses if they have a balance owing of more than $500.
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Registration_Insert_BalanceOwing]'))
    DROP TRIGGER Registration_Insert_BalanceOwing
GO

CREATE TRIGGER Registration_Insert_BalanceOwing
ON Registration
FOR Insert
AS
    -- Body of Trigger
    IF @@ROWCOUNT > 0 AND
       EXISTS(SELECT S.StudentID FROM inserted I INNER JOIN  Student S ON I.StudentID = S.StudentID
              WHERE S.BalanceOwing > 500)
    BEGIN
        RAISERROR('Student owes too much money - cannot register student in course', 16, 1)
        ROLLBACK TRANSACTION
    END
RETURN
GO
-- TODO: Write code to test this trigger
SELECT * FROM Student WHERE BalanceOwing > 0

-- Write a stored procedure called RegisterStudent that puts a student in a course and increases the balance owing by the cost of the course.
-- After creating this stored procedure, do some tests of the stored procedure. Remember to have the trigger in place (on the Registration table) before your test your stored procedure.
-- TODO: Student Answer Here

--4. Our school DBA has suddenly disabled some Foreign Key constraints to deal with performance issues! Create a trigger on the Registration table to ensure that only valid CourseIDs, StudentIDs and StaffIDs are used for grade records. (You can use sp_help tablename to find the name of the foreign key constraints you need to disable to test your trigger.) Have the trigger raise an error for each foreign key that is not valid. If you have trouble with this question create the trigger so it just checks for a valid student ID.
-- Notes: Foreign Keys give us our "referential integrity" in our database.
-- sp_help Registration -- then disable the foreign key constraints....
ALTER TABLE Registration NOCHECK CONSTRAINT FK_GRD_CRS_CseID
ALTER TABLE Registration NOCHECK CONSTRAINT FK_GRD_STF_StaID
ALTER TABLE Registration NOCHECK CONSTRAINT FK_GRD_STU_StuID
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Registration_InsertUpdate_EnforceForeignKeyValues]'))
    DROP TRIGGER Registration_InsertUpdate_EnforceForeignKeyValues
GO

CREATE TRIGGER Registration_InsertUpdate_EnforceForeignKeyValues
ON Registration
FOR Insert,Update -- Choose only the DML statement(s) that apply
AS
	-- Body of Trigger
    IF @@ROWCOUNT > 0 -- As a rule of thumb, you should always check for @@ROWCOUNT in a trigger
    BEGIN
        -- UPDATE(columnName) is a function call that checks to see if information between the 
        -- deleted and inserted tables for that column are different (i.e.: data in that column
        -- has changed).
        IF UPDATE(StudentID) AND
           NOT EXISTS (SELECT * FROM inserted I INNER JOIN Student S ON I.StudentID = S.StudentID)
        BEGIN
            RAISERROR('That is not a valid StudentID', 16, 1)
            ROLLBACK TRANSACTION
        END
        ELSE
        IF UPDATE(CourseID) AND
           NOT EXISTS (SELECT * FROM inserted I INNER JOIN Course C ON I.CourseId = C.CourseId)
        BEGIN
            RAISERROR('That is not a valid CourseID', 16, 1)
            ROLLBACK TRANSACTION
        END
        ELSE
        IF UPDATE(StaffID) AND
           NOT EXISTS (SELECT * FROM inserted I INNER JOIN Staff S ON I.StaffID = S.StaffID)
        BEGIN
            RAISERROR('That is not a valid StaffID', 16, 1)
            ROLLBACK TRANSACTION
        END
    END
RETURN
GO
-- Test the trigger (on Registration table)
SELECT * FROM Registration
-- Let's modify this record: 199899200	DMIT254	2005M	0.00	Y	6
-- Testing with good data
UPDATE  Registration
SET     Mark = 65,
        WithdrawYN = 'N'
WHERE   StudentID = 199899200
  AND   CourseId = 'DMIT254'
  AND   SEMESTER = '2005M'
-- Testing with bad data
UPDATE  Registration
SET     StaffID = 99
WHERE   StudentID = 199899200
  AND   CourseId = 'DMIT254'
  AND   SEMESTER = '2005M'

-- 5. The school has placed a temporary hold on the creation of any more clubs. (Existing clubs can be renamed or removed, but no additional clubs can be created.) Put a trigger on the Clubs table to prevent any new clubs from being created.
IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Club_Insert_Lockdown]'))
    DROP TRIGGER Club_Insert_Lockdown
GO

CREATE TRIGGER Club_Insert_Lockdown
ON Club
FOR Insert -- Choose only the DML statement(s) that apply
AS
	-- Body of Trigger
    IF @@ROWCOUNT > 0
    BEGIN
        RAISERROR('Temporary lockdown on creating new clubs.', 16, 1)
        ROLLBACK TRANSACTION
    END
RETURN
GO
INSERT INTO Club(ClubId, ClubName) VALUES ('HACK', 'Honest Analyst Computer Knowledge')
GO
-- 6. Our network security officer suspects our system has a virus that is allowing students to alter their balance owing records! In order to track down what is happening we want to create a logging table that will log any changes to the balance owing in the Student table. You must create the logging table and the trigger to populate it when the balance owing is modified.
-- For this problem, we only need the trigger to work on the UPDATE statement, as inserting does not change an old balance and deleting does not generate a new balance.
-- Step 1) Make the logging table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BalanceOwingLog')
    DROP TABLE BalanceOwingLog
GO
CREATE TABLE BalanceOwingLog
(
    LogID           int  IDENTITY (1,1) NOT NULL CONSTRAINT PK_BalanceOwingLog PRIMARY KEY,
    StudentID       int                 NOT NULL,
    ChangeDateTime  datetime            NOT NULL,
    OldBalance      money               NOT NULL,
    NewBalance      money               NOT NULL
)
GO

IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Student_Update_AuditBalanceOwing]'))
    DROP TRIGGER Student_Update_AuditBalanceOwing
GO

CREATE TRIGGER Student_Update_AuditBalanceOwing
ON Student
FOR Update -- Choose only the DML statement(s) that apply
AS
	-- Body of Trigger
    IF @@ROWCOUNT > 0 AND UPDATE(BalanceOwing) -- these are the only two conditions we're concerned about for logging
	BEGIN
	    INSERT INTO BalanceOwingLog (StudentID, ChangedateTime, OldBalance, NewBalance)
	    SELECT I.StudentID, GETDATE(), D.BalanceOwing, I.BalanceOwing
        FROM deleted D INNER JOIN inserted I on D.StudentID = I.StudentID
        WHERE  D.BalanceOwing <> I.BalanceOwing -- the reason for this line here is that our UPDATE statement might affect multiple rows, and not all of the rows will have a change in the BalanceOwing.
	    IF @@ERROR <> 0 
	    BEGIN
		    RAISERROR('Insert into BalanceOwingLog Failed',16,1)
            ROLLBACK TRANSACTION
		END	
	END
RETURN
GO

SELECT * FROM BalanceOwingLog -- To see what's in there before an update
-- Hacker statements happening offline....
UPDATE Student SET BalanceOwing = BalanceOwing - 100 -- Hacker failed, but not disuaded
UPDATE Student SET BalanceOwing = BalanceOwing - 100 WHERE BalanceOwing > 100
SELECT * FROM BalanceOwingLog -- To see what's in there after a hack attempt
UPDATE Student SET BalanceOwing = 10000 -- He's graduated, and doesn't want competition
SELECT * FROM BalanceOwingLog -- To see what's in there after a hack attempt



