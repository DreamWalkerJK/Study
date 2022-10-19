using MvcStudy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MvcStudy.DAL
{
    public class SchoolInitializer : DropCreateDatabaseIfModelChanges<SchoolDbContext>
    {
        protected override void Seed(SchoolDbContext context)
        {
            var students = new List<Student>
            {
                new Student{Name = "ZhangSan", EnrollmentDate = DateTime.Parse("2005-09-01")},
                new Student{Name = "LiSi", EnrollmentDate = DateTime.Parse("2006-09-01")},
                new Student{Name = "WangWu", EnrollmentDate = DateTime.Parse("2007-09-01")},
                new Student{Name = "LiYi", EnrollmentDate = DateTime.Parse("2007-09-01")},
                new Student{Name = "WangBing", EnrollmentDate = DateTime.Parse("2006-09-01")},
                new Student{Name = "ChenDing", EnrollmentDate = DateTime.Parse("2005-09-01")}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course{CourseId = 1001, CourseName = "Chinses", CourseCredits = 4},
                new Course{CourseId = 1002, CourseName = "Math", CourseCredits = 4},
                new Course{CourseId = 1003, CourseName = "English", CourseCredits = 4},
                new Course{CourseId = 1004, CourseName = "Physic", CourseCredits = 3},
                new Course{CourseId = 1005, CourseName = "Chemistry", CourseCredits = 3},
                new Course{CourseId = 1006, CourseName = "Biotechnology", CourseCredits = 3},
                new Course{CourseId = 1007, CourseName = "History", CourseCredits = 3},
                new Course{CourseId = 1008, CourseName = "Politics", CourseCredits = 3},
                new Course{CourseId = 1009, CourseName = "Geography", CourseCredits = 3}
            };

            courses.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{ StudentId = 1, CourseId = 1001, Grade = Grade.A},
                new Enrollment{ StudentId = 1, CourseId = 1002, Grade = Grade.B},
                new Enrollment{ StudentId = 1, CourseId = 1003},

                new Enrollment{ StudentId = 6, CourseId = 1001, Grade = Grade.A},
                new Enrollment{ StudentId = 6, CourseId = 1002, Grade = Grade.C},
                new Enrollment{ StudentId = 6, CourseId = 1003},

                new Enrollment{ StudentId = 2, CourseId = 1004, Grade = Grade.A},
                new Enrollment{ StudentId = 2, CourseId = 1005, Grade = Grade.A},
                new Enrollment{ StudentId = 2, CourseId = 1006},

                new Enrollment{ StudentId = 5, CourseId = 1004, Grade = Grade.B},
                new Enrollment{ StudentId = 5, CourseId = 1005, Grade = Grade.B},
                new Enrollment{ StudentId = 5, CourseId = 1006},

                new Enrollment{ StudentId = 3, CourseId = 1007, Grade = Grade.B},
                new Enrollment{ StudentId = 3, CourseId = 1008, Grade = Grade.A},
                new Enrollment{ StudentId = 3, CourseId = 1009},

                new Enrollment{ StudentId = 4, CourseId = 1007, Grade = Grade.B},
                new Enrollment{ StudentId = 4, CourseId = 1008, Grade = Grade.C},
                new Enrollment{ StudentId = 4, CourseId = 1009}
            };

            enrollments.ForEach(e => context.Enrollments.Add(e));
            context.SaveChanges();
        }
    }
}