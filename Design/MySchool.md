# My School

> This sample normalization contains the final entities from performing 0NF through 3NF on various views for a school offering courses. The printable version is available [here](./MySchool.pdf).

**TODO:**

- [ ] Expand Class Schedule View to be all the class schedules for an instructor.

## Course Offering View

### Final Entities

**CourseOffering:** <span class="md"><b class="pk">OfferingId</b>, <u class="fk">CourseNumber</u>, SectionNumber, StartDate, EndDate, <u class="fk">InstructorId</u></span>

**ClassMembers:** <span class="md"><b class="pk"><u class="fk">OfferingId</u>, <u class="fk">StudentId</u></b>, Paid</span>

**Students:** <span class="md"><b class="pk">StudentId</b>, FirstName, LastName, Email</span>

**Instructors:** <span class="md"><b class="pk">InstructorId</b>, FirstName, LastName, Sessional</span>

**Courses:** <span class="md"><b class="pk">CourseNumber</b>, Name, Credits, Hours)

----

## Class Shedule View

### Final Entities

**OfferingSchedules:** <span class="md"><b class="pk">CourseOfferingId</b>, <u class="fk">CourseNumber</u>, StartDate, EndDate</span>

**Rooms:** <span class="md"><b class="pk"><u class="fk">CourseOfferingId</u>, DayOfWeek</b>, StartTime, EndTime, RoomNumber</span>

**Courses:** <span class="md"><b class="pk">CourseNumber</b>, SectionNumber</span>

----

## Term Holidays View

**SchoolTerms:** <span class="md"><b class="pk">TermNumber</b>, StartDate, EndDate

**Holidays:** <span class="md"><b class="pk"><u class="fk">TermNumber</u>, Description</b>, FromDate, ToDate</span>

----

<style>
.md {
    display: inline-block;
    vertical-align: top;
    white-space:normal;
}
.md::before {
    content: '(';
    font-size: 1.25em;
    font-weight: bold;
}
.md::after {
    content: ')';
    font-size: 1.25em;
    font-weight: bold;
}
.pk {
    font-weight: 700;
    display: inline-block;
    border: thin solid #00f;
    padding: 0 2px;
    position: relative;
}
.pk::before {
    content: 'P';
    font-size:.55em;
    font-weight: bold;
    color: white;
    background-color: #72c4f7;
    position: absolute;
    left: -5px;
    top: -15px;
    border-radius: 50%;
    border: solid thin blue;
    width: 1.4em;
    height: 1.4em;
    padding:3px;
    text-align:center;
}
.fk {
    color: green;
    font-style: italic;
    text-decoration: wavy underline green;
    padding: 0 2px;
    position: relative;
}
.fk::before {
    content: 'F';
    font-size:.65em;
    position: absolute;
    left: -1px;
    bottom: -17px;
    color:darkgreen;
    background-color: #a7dea7;
    border-radius: 50%;
    border: dashed thin green;
    width: 1.4em;
    height: 1.4em;
    padding:3px;
    text-align:center;
}
.rg::before {
    content: '\007B';
    color: darkorange;
    font-size: 1.2em;
    font-weight: bold;
}
.rg::after {
    content: '\007D';
    color: darkorange;
    font-size: 1.2em;
    font-weight: bold;
}
.rg {
    display: inline-block;
    color: inherit;
    font-size: 1em;
    font-weight: normal;
}
.note {
    font-weight: bold;
    color: brown;
    font-size: 1.1em;
}
</style>
