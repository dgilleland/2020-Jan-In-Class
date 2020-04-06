using GradeBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.DataModels
{
    public class UpcomingCourse
    {
        internal string SectionName;

        public TermStart SchoolTerm { get; internal set; }
        public int CourseId { get; internal set; }
    }
}
