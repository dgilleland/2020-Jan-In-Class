using GradeBook.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.DAL
{
    internal class GradebookContext : DbContext
    {
        public GradebookContext() : base("DefaultConnection") { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseOffering> CourseOfferings { get; set; }
        public DbSet<CourseSection> CourseSections { get; set; }
        public DbSet<ClassMember> ClassMembers { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
