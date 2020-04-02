using System.Collections.Generic;

namespace GradeBook.Entities
{
    internal class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public virtual ICollection<CourseOffering> Offerings { get; set; }

        public Course()
        {
            Offerings = new HashSet<CourseOffering>();
        }
    }
}
