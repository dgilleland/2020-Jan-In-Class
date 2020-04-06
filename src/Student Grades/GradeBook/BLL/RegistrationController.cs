using GradeBook.DAL;
using GradeBook.DataModels;
using GradeBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
Code-Behind:
    - Managing the different parts of the UI on the page
        - Recognize when the controls are children of the Page itself, and when they are children of another control; controls inside a GridView/ListView/Repeater are children of that control, not the page.
    - how do we grab controls from gridviews and listviews?
        - You need to get a reference to the GridViewRow or the ListViewItem in the Gridview/ListView, and then you use a .FindControl("") to get the child control.
    - Code behind issues when grabbing controls? (typically run-time problems)
        - Get comfortable with running in Debug mode and using breakpoints AND the immediate window
    - do we have access to variables that persist over postbacks that don't involve querying the database? I'm still foggy on what stays when we use the code behind
        - We should consider all interaction with the browser as being "stateless" - we don't expect data to persist over a postback. HTTP is considered a "stateless protocol"
        - Note however, that many <asp:> controls do manage their own state after postback. These controls maintain state by using a <input type="hidden"> on the page with information on what the controls had for data when the page was requested by the browser. Then, they do a comparison (under the hood) of what was sent on the last page request and what the input elements have on the postback.
BLL Processing:
    - BLL Validations (what are you validating?)
    - i just need a little bit more clarity on Processing as a single transaction
Best Practices:
    - About validation, should we do it in the code behind of the page or on the BLL method before we process?
        A) You should always do it in the BLL method, and for the code-behind, it's a matter of only doing validation that focuses on 1) correct data types (if it applies), 2) friendly user experience/feedback, 3) that you have relatively "complete" information.
           The job of the PL is to gather data from the user to send to the BLL
           The BLL has to make sure the data meets all the business requirements. When you code your BLL method, keep in mind that the BLL has no idea where the data came from.
    - When we send things to the BLL is it best to packaged everything into one item?
        A) Sometimes yes, sometimes no. Try not to make your BLL method have too many parameters. Make sure to minimize the number of primitive types sent into your BLL method.
Tips:
    - You should pick one language (C#, PHP) to do a "deep dive" on.
*/
namespace GradeBook.BLL
{
    public class RegistrationController
    {
        #region Applicants
        public IEnumerable<Applicant> ListFakeStudents()
        {
            yield return new Applicant() { LastName = "Toll", FirstName = "Sofia" };
            yield return new Applicant() { LastName = "Arrigo", FirstName = "Alexis" };
            yield return new Applicant() { LastName = "Jackson", FirstName = "Cal" };
            yield return new Applicant() { LastName = "Mcardle", FirstName = "Laurence" };
            yield return new Applicant() { LastName = "Taylor", FirstName = "Lahoma" };
            yield return new Applicant() { LastName = "Graziano", FirstName = "Jonathan" };
            yield return new Applicant() { LastName = "Oleary", FirstName = "Nan" };
            yield return new Applicant() { LastName = "Goodridge", FirstName = "Jude" };
            yield return new Applicant() { LastName = "Gross", FirstName = "Janine" };
            yield return new Applicant() { LastName = "Bush", FirstName = "Helvetius" };
            yield return new Applicant() { LastName = "Coronilla", FirstName = "Marco" };
            yield return new Applicant() { LastName = "Bosher", FirstName = "Taylor" };
            yield return new Applicant() { LastName = "Gonzalez", FirstName = "Cody" };
            yield return new Applicant() { LastName = "Irving", FirstName = "Marshall" };
            yield return new Applicant() { LastName = "Zocchi", FirstName = "Tomas" };
            yield return new Applicant() { LastName = "Kelmis", FirstName = "Courtney" };
            yield return new Applicant() { LastName = "Mcguffin", FirstName = "Isabelle" };
            yield return new Applicant() { LastName = "Bevins", FirstName = "India" };
            yield return new Applicant() { LastName = "Custis", FirstName = "Judge" };
            yield return new Applicant() { LastName = "Zibaila", FirstName = "Alan-Michael" };
            yield return new Applicant() { LastName = "Rowings", FirstName = "Janene" };
            yield return new Applicant() { LastName = "Hook", FirstName = "Livia" };
            yield return new Applicant() { LastName = "Checkley", FirstName = "Tad" };
            yield return new Applicant() { LastName = "Katterman", FirstName = "Madison" };
            yield return new Applicant() { LastName = "Lamson", FirstName = "Margaret" };
            yield return new Applicant() { LastName = "Jobe", FirstName = "Sylvie" };
            yield return new Applicant() { LastName = "Cucullu", FirstName = "Laura" };
            yield return new Applicant() { LastName = "Renaud", FirstName = "Xavier" };
            yield return new Applicant() { LastName = "Stillwell", FirstName = "Cass" };
            yield return new Applicant() { LastName = "Hanson", FirstName = "Leanna" };
        }
        #endregion

        #region Commands
        public void RegisterStudent(Applicant newStudent, List<UpcomingCourse> courses)
        {
            // Precondition validations - I should not have NULL objects to work with
            if (newStudent == null)
                throw new ArgumentNullException(nameof(newStudent),
                                                "Applicant information is required for registering new students");
            if (courses == null || courses.Count == 0 || courses.All(x => x == null))
                throw new ArgumentException("There are no courses to register the new student into",
                                            nameof(courses));
            else if (courses.Any(x => x == null))
                throw new ArgumentNullException("Some of the course information is not supplied (null values)");

            using (var context = new GradebookContext())
            {
                #region Validation
                /* 0) Validation
                 * This is where we are interested in making sure that a) the data coming in is "good" (it will "fit" with our current state of the database) for the task we want to perform and b) we are enforcing any business rules that apply to our task.
                 */
                var errors = new List<Exception>();
                foreach(var course in courses)
                {
                    var foundSection =
                        context
                        .CourseSections
                        .SingleOrDefault(x => x.Name == course.SectionName // section name
                                           && x.CourseOffering.CourseId == course.CourseId // course id
                                           && x.CourseOffering.Term == course.SchoolTerm); // school term
                    if (foundSection == null)
                        errors.Add(new Exception($"A section {course.SectionName} for the course {course.CourseId} in term {course.SchoolTerm} could not be found"));
                }
                if (errors.Any()) // If there are any errors
                    ; // TODO: throw new BusinesssRuleException($"Unable to register {newStudent.FirstName} {newStudent.LastName} for some courses", errors);
                #endregion

                #region Processing
                /* 1) Process the data to put it into the database
                 * Part of the key is remember that we are NOT doing
                 * SQL. Don't trying to manage the Foreign Keys if they're already being managed by the database context.
                 * a) Use our context object to get/work with the Entity/ies that's at the core of the task.
                 * b) We want to do this as a SINGLE TRANSACTION. Just do a single call to context.SaveChanges();
                 */
                // 1.1 - Create/Add our student
                var freshman = new Student { FirstName = newStudent.FirstName, LastName = newStudent.LastName };
                // Get the next available SchoolId (assumptions: the IDs are numeric, even though they are in a string)
                var nextId = context.Students.Max(x => int.Parse(x.SchoolId)) + 1; // "pretend"
                freshman.SchoolId = nextId.ToString();

                // 1.2 - Find the courses (for the section)
                foreach(var course in courses)
                {
                    // Find the actual section for the course, given ...
                    var foundSection = 
                        context
                        .CourseSections
                        .SingleOrDefault(x => x.Name == course.SectionName // section name
                                           && x.CourseOffering.CourseId == course.CourseId // course id
                                           && x.CourseOffering.Term == course.SchoolTerm); // school term
                    // 1.3 - Add the student as a class member in those courses
                    var membership = new ClassMember { CourseSectionId = foundSection.CourseSectionId };
                    freshman.Classes.Add(membership); // the membership.StudentID will get figured out by EF
                }

                // 1.4 - Process as a transaction
                context.Students.Add(freshman);
                context.SaveChanges(); // Here, if there are NEW rows being added and any PKs to be generated by the database, then Entity Framework will figure out all the relationships in our freshman and the class memberships
                #endregion
            }
        }
        #endregion
    }
}
