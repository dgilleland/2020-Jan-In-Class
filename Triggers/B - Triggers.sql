-- Triggers Practice
USE [A01-School]
GO

-- A. The Registration table has a composite primary key. In order to ensure that parts of this key cannot be changed, write a trigger called Registration_ProtectPrimaryKey that will prevent changes to the primary key columns.


-- B. Create a trigger called Registration_InstructorLoad to ensure that an instructor does not teach more than 3 courses in a given semester. Be sure to account for the fact that the staff assigned to a course can be changed part-way through the semester.


-- C. Create a trigger called Registration_ClassSizeLimit to ensure that students cannot be added to a course if the course is already full. Be sure to account for students who have withdrawn.


-- D. (Advanced/Impossible?) Change the Registration_ClassSizeLimit trigger so students will be added to a wait list if the course is already full; make sure the student is not added to Registration, and include a message that the student has been added to a waitlist. You should design a WaitList table to accommodate the changes needed for adding a student to the course once space is freed up for the course. Students should be added on a first-come-first-served basis (i.e. - include a timestamp in your WaitList table)


-- E. (Advanced) Create a trigger called Registration_AutomaticEnrollment that will add students from the wait list of a course whenever another student withdraws