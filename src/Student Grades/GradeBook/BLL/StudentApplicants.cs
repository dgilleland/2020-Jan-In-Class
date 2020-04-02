using GradeBook.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
