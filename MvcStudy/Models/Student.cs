using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcStudy.Models
{
    public class Student : Person
    {
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Student FullId")]
        public string StudentFullId
        {
            get
            {
                return "S" + EnrollmentDate.ToString("yyyy") + Id.ToString();
            }
        }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}