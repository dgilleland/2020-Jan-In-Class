/* *************
 * File: SchoolTranscript.sql
 * Author: Dan Gilleland
 *
 *  CREATE DATABASE SchoolTranscript
 ************ */
USE SchoolTranscript
GO

/* === Drop Statements === */
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'StudentCourses')
    DROP TABLE StudentCourses
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Courses')
    DROP TABLE Courses
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Students')
    DROP TABLE Students


/* === Create Tables === */
CREATE TABLE Students
(
    StudentID       int             NOT NULL,
    GivenName       varchar(50)     NOT NULL,
    Surname         varchar(50)     NOT NULL,
    DateOfBirth     datetime        NOT NULL,
    Enrolled        bit             NOT NULL
)

CREATE TABLE Courses
(
    Number          varchar(10)     NOT NULL,
    Name            varchar(50)     NOT NULL,
    Credits         decimal(3, 1)   NOT NULL,
)
