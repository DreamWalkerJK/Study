using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcStudy.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        
        [Display(Name = "Department Name")]
        [StringLength(50, MinimumLength = 3)]
        public string DepartmentName { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }
        
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public int? InstructorId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}