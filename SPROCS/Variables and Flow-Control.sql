-- Variables and Flow Control

-- Declare a variable
DECLARE @Cost money
-- Set a value for the variable using a value from the database
-- Note that the whole SELECT statement is in parenthesis
SET @Cost = (SELECT CourseCost FROM Course WHERE CourseId = 'DMIT101')
-- Note in the above, since our variable can only hold a single value, then
-- the subquery that we're using can only return a single row and a single column
PRINT @Cost
DECLARE @Name varchar(40)
-- We can set the value in a variable through a query. In this case, we must
-- still ensure that there is a SINGLE value being stored in the variable.
-- In other words, we can't have multiple rows coming back from the query.
SELECT  @Name = CourseName,
        @Cost = CourseCost
FROM    Course
WHERE   CourseId = 'DMIT152'
PRINT 'The name is ' 
PRINT @Name
PRINT ' and the cost is $' 
PRINT @Cost


-- Understanding BEGIN/END blocks
--  A BEGIN/END block basically acts like a pair of curly braces in C#.
--  It represents a single block of code, that is, a single set of instructions.
--  These are helpful especially with the IF/ELSE flow-control statements.
--  Consider the following example.
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'GuessRows')
    DROP PROCEDURE GuessRows
GO
CREATE PROCEDURE GuessRows
    @clubRows   int
AS
    DECLARE @actual int
    SELECT @actual = COUNT(ClubId) FROM Club
    IF @actual <> @clubRows
    BEGIN
        RAISERROR('Wrong guess. Club has a different number of rows', 16, 1)
        IF @clubRows > @actual
            RAISERROR('Too high a guess', 16, 1)
        ELSE
            RAISERROR('Too low a guess', 16, 1)
    END
    ELSE
    BEGIN
        RAISERROR('Good guess!', 16, 1)
    END
RETURN
GO
EXEC GuessRows 5
