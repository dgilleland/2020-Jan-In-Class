using System.Collections.Generic;

namespace GradeBook.Entities
{
    internal class CourseOffering
    {
        public int CourseOfferingId { get; set; }

        public int CourseId { get; set; }
        public int Year { get; set; }
        public TermStart Term { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<CourseSection> Sections { get; set; }

        public CourseOffering()
        {
            Sections = new HashSet<CourseSection>();
        }
    }
}
