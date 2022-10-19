using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcStudy.Models
{
    public class Course
    {
        [Display(Name = "Course Number")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string CourseName { get; set; }

        [Range(0,5)]
        public int CourseCredits { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}