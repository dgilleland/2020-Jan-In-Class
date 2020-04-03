using GradeBook.DataModels;
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
    }
}
