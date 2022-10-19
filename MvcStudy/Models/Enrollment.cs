using System.ComponentModel.DataAnnotations;

namespace MvcStudy.Models
{
    public enum Grade
    {
        A,
        B,
        C,
        D,
        F
    }
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }

        [DisplayFormat(NullDisplayText = "No Grade")]
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}