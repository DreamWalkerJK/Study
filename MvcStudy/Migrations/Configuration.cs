namespace MvcStudy.Migrations
{
    using MvcStudy.DAL;
    using MvcStudy.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcStudy.DAL.SchoolDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcStudy.DAL.SchoolDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var students = new List<Student>
            {
                new Student { Name = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { Name = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Name = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { Name = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Name = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Name = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { Name = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { Name = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01") }
            };

            students.ForEach(s => context.Students.AddOrUpdate(t => t.Name, s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { Name = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { Name = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { Name = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { Name = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { Name = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { DepartmentName = "Major",     Budget = 350000,
                    StartDate = DateTime.Parse("1995-09-01"),
                    InstructorId  = instructors.Single( i => i.Name == "Abercrombie").Id },
                new Department { DepartmentName = "Science", Budget = 100000,
                    StartDate = DateTime.Parse("1995-09-01"),
                    InstructorId  = instructors.Single( i => i.Name == "Fakhouri").Id },
                new Department { DepartmentName = "Liberal", Budget = 350000,
                    StartDate = DateTime.Parse("1995-09-01"),
                    InstructorId  = instructors.Single( i => i.Name == "Harui").Id },
                new Department { DepartmentName = "Minor",   Budget = 100000,
                    StartDate = DateTime.Parse("1995-09-01"),
                    InstructorId  = instructors.Single( i => i.Name == "Kapoor").Id }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.DepartmentName, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course
                {
                    CourseId = 1001,
                    CourseName = "Chinese",
                    CourseCredits = 5,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Major").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1002,
                    CourseName = "Math",
                    CourseCredits = 5,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Major").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1003,
                    CourseName = "English",
                    CourseCredits = 5,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Major").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1004,
                    CourseName = "Physics",
                    CourseCredits = 3,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Science").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1005,
                    CourseName = "Chemistry",
                    CourseCredits = 3,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Science").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1006,
                    CourseName = "Biology",
                    CourseCredits = 3,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Science").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1007,
                    CourseName = "Politics",
                    CourseCredits = 3,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Liberal").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1008,
                    CourseName = "History",
                    CourseCredits = 3,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Liberal").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1009,
                    CourseName = "Geography",
                    CourseCredits = 3,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Liberal").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1010,
                    CourseName = "Music",
                    CourseCredits = 1,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Minor").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1011,
                    CourseName = "Arts",
                    CourseCredits =1,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Minor").DepartmentId,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseId = 1012,
                    CourseName = "Sport",
                    CourseCredits =1,
                    DepartmentId = departments.Single( s => s.DepartmentName == "Minor").DepartmentId,
                    Instructors = new List<Instructor>()
                }
            };

            courses.ForEach(c => context.Courses.AddOrUpdate(o => o.CourseName, c));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    InstructorId = instructors.Single( i => i.Name == "Fakhouri").Id,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    InstructorId = instructors.Single( i => i.Name == "Harui").Id,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    InstructorId = instructors.Single( i => i.Name == "Kapoor").Id,
                    Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorId, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Chinese", "Kapoor");
            AddOrUpdateInstructor(context, "Sport", "Harui");
            AddOrUpdateInstructor(context, "Math", "Zheng");
            AddOrUpdateInstructor(context, "Physics", "Zheng");

            AddOrUpdateInstructor(context, "Music", "Fakhouri");
            AddOrUpdateInstructor(context, "Arts", "Harui");
            AddOrUpdateInstructor(context, "Politics", "Abercrombie");
            AddOrUpdateInstructor(context, "History", "Abercrombie");

            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentId = students.Single(s => s.Name == "Alexander").Id,
                    CourseId = courses.Single(c => c.CourseName == "Music" ).CourseId,
                    Grade = Grade.A
                },
                 new Enrollment {
                    StudentId = students.Single(s => s.Name == "Alexander").Id,
                    CourseId = courses.Single(c => c.CourseName == "Arts" ).CourseId,
                    Grade = Grade.C
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.Name == "Alexander").Id,
                    CourseId = courses.Single(c => c.CourseName == "Sport" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentId = students.Single(s => s.Name == "Alonso").Id,
                    CourseId = courses.Single(c => c.CourseName == "Music" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentId = students.Single(s => s.Name == "Alonso").Id,
                    CourseId = courses.Single(c => c.CourseName == "Arts" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.Name == "Alonso").Id,
                    CourseId = courses.Single(c => c.CourseName == "Sport" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.Name == "Anand").Id,
                    CourseId = courses.Single(c => c.CourseName == "Music" ).CourseId
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.Name == "Anand").Id,
                    CourseId = courses.Single(c => c.CourseName == "Arts").CourseId,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentId = students.Single(s => s.Name == "Barzdukas").Id,
                    CourseId = courses.Single(c => c.CourseName == "Music").CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.Name == "Li").Id,
                    CourseId = courses.Single(c => c.CourseName == "Arts").CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.Name == "Justice").Id,
                    CourseId = courses.Single(c => c.CourseName == "Sport").CourseId,
                    Grade = Grade.B
                 }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                        s.StudentId == e.StudentId &&
                        s.Course.CourseId == e.CourseId).SingleOrDefault();

                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }

            context.SaveChanges();
        }

        void AddOrUpdateInstructor(SchoolDbContext context, string courseName, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.CourseName == courseName);
            var inst = crs.Instructors.SingleOrDefault(i => i.Name == instructorName);

            if(inst == null)
            {
                crs.Instructors.Add(context.Instructors.Single(i => i.Name == instructorName));
            }
        }
    }
}
