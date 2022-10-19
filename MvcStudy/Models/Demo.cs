using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcStudy.Models
{
    public enum ColorType
    {
        Black,
        White,
        Grey
    }
    public class Demo
    {
        public int DemoId { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string DemoName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        public string DemoOther { get; set; }

        [Range(1,100)]
        [DataType(DataType.Currency)]
        public decimal DemoPrice { get; set; }

        public ColorType DemoColor { get; set; }

        [Display(Name = "DemoDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DemoDate { get; set; }
    }

    public class MvcStudyDbContext : DbContext
    {
        public DbSet<Demo> Demoes { get; set; }
    }
}