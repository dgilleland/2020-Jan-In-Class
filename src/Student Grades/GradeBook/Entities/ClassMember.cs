using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Entities
{
    internal class ClassMember
    {
        [Key, Column(Order = 1)]
        public int CourseSectionId { get; set; }
        [Key, Column(Order = 2)]
        public int StudentId { get; set; }

        public double? Mark { get; set; }

        public virtual CourseSection CourseSection { get; set; }
        public virtual Student Student { get; set; }
    }
}
