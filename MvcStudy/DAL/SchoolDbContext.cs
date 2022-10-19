using MvcStudy.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MvcStudy.DAL
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext() : base("MvcStudySchoolDbContext")
        {

        }

        public DbSet<Person> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // fluent API
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseId")
                .MapRightKey("Id")
                .ToTable("CourseInstructor"));

            modelBuilder.Entity<Department>()
                .Property(p => p.RowVersion).IsConcurrencyToken();

            modelBuilder.Entity<Department>().MapToStoredProcedures();
        }
    }
}