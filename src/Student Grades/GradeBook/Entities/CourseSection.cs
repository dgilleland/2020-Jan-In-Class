using System.Collections.Generic;

namespace GradeBook.Entities
{
    internal class CourseSection
    {
        public int CourseSectionId { get; set; }
        public int CourseOfferingId { get; set; }
        public string Name { get; set; }

        public virtual CourseOffering CourseOffering { get; set; }
        public virtual ICollection<ClassMember> Students { get; set; }

        public CourseSection()
        {
            Students = new HashSet<ClassMember>();
        }
    }
}
