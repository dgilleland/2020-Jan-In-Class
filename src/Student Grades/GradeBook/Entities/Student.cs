using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.Entities
{
    internal class Student
    {
        public int StudentId { get; set; }
        public string SchoolId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<ClassMember> Classes { get; set; }
    }
}
