--Subquery Exercise - Answers
--Use the IQSchool database for this exercise. Each question must use a subquery in its solution.
--**If the questions could also be solved without a subquery, solve it without one as well**
USE [A01-School]
GO

--2. Select The Student ID's of all the students that are in the 'Association of Computing Machinery' club
-- TODO: Student Answer Here
SELECT  StudentID
FROM    Activity
WHERE   ClubID = (SELECT ClubID FROM Club
                  WHERE ClubName = 'Association of Computing Machinery')

-- 2.b. Select the names of all the students in the 'Association of Computing Machinery' club. Use a subquery for your answer. When you make your answer, ensure the outmost query only uses the Student table in its FROM clause.
SELECT  FirstName + ' ' + LastName
FROM    Student
WHERE   StudentID IN
       (SELECT  StudentID
        FROM    Activity
        WHERE   ClubID = (SELECT ClubID FROM Club
                          WHERE ClubName = 'Association of Computing Machinery'))

--4. Select All the staff full names that taught DMIT172.
-- TODO: Student Answer Here
SELECT FirstName + ' ' + LastName AS 'Staff'
FROM   Staff
WHERE  StaffID IN -- I used IN because the subquery returns many rows
    (SELECT DISTINCT StaffID FROM Registration WHERE CourseId = 'DMIT172')

-- The above can also be done as an INNER JOIN...
SELECT DISTINCT FirstName + ' ' + LastName AS 'Staff'
FROM Staff
    INNER JOIN Registration
        ON Staff.StaffID = Registration.StaffID
WHERE CourseId = 'DMIT172'


-- 9. What is the avg mark for each of the students from Edm? Display their StudentID and avg(mark)
-- TODO: Student Answer Here...
SELECT  S.StudentID, AVG(Mark) AS 'Average Mark'
FROM    Student AS S
    INNER JOIN Registration AS R
        ON S.StudentID = R.StudentID
WHERE   City = 'Edm'
GROUP BY S.StudentID

-- 10. Which student(s) have the highest average mark? Hint - This can only be done by a subquery.
-- TODO: Student Answer Here...
SELECT StudentID
FROM   Registration
GROUP BY StudentID
HAVING AVG(Mark) >= ALL --  A number can't be 'GREATER THAN or EQUAL TO' a NULL value
        (SELECT AVG(Mark)
         FROM   Registration
         WHERE  Mark IS NOT NULL -- Ah, tricky!
         GROUP BY StudentID)

-- 11. Which course(s) allow the largest classes? Show the course id, name, and max class size.
-- TODO: Student Answer Here...
SELECT  CourseId, CourseName, MaxStudents
FROM    Course
WHERE   MaxStudents >= ALL (SELECT MaxStudents FROM Course)

-- 12. Which course(s) are the most affordable? Show the course name and cost.
-- TODO: Student Answer Here...
SELECT  CourseName, CourseCost
FROM    Course
WHERE   CourseCost <= ALL (SELECT CourseCost FROM Course)

-- 13. Which staff have taught the largest classes? (Be sure to group registrations by course and semester.)
-- TODO: Student Answer Here...
SELECT  DISTINCT FirstName + ' ' + LastName AS 'StaffName'
        --, CourseId
        --, COUNT(CourseId)
FROM    Staff AS S
    INNER JOIN Registration AS R
        ON S.StaffID = R.StaffID
GROUP BY FirstName + ' ' + LastName, CourseId
HAVING  COUNT(CourseId) >= ALL (SELECT COUNT(CourseId)
                                FROM   Registration
                                GROUP BY StaffID, CourseId)

-- 14. Which students are most active in the clubs?
-- TODO: Student Answer Here...
SELECT  FirstName + ' ' + LastName
      --, COUNT(A.ClubId)
FROM    Student AS S
    INNER JOIN Activity AS A
        ON S.StudentID = A.StudentID
GROUP BY FirstName + ' ' + LastName
HAVING  COUNT(ClubId) >= ALL (SELECT COUNT(ClubId) FROM Activity GROUP BY StudentID)
