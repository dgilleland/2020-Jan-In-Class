using GradeBook.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.BLL
{
    [DataObject]
    public class CourseCatalogController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ActiveCourse> ListActiveCourses()
        {
            //throw new NotImplementedException();
            var fakeResult = new List<ActiveCourse>();
            fakeResult.Add(new ActiveCourse { CourseId = 1, Name = "Programming Fundamentals", Number = "CPSC1012" });
            fakeResult.Add(new ActiveCourse { CourseId = 2, Name = "Database Fundamentals", Number = "DMIT1508" });
            return fakeResult;
        }
    }
}
