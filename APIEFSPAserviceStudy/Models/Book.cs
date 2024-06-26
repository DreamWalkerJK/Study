﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace APIEFSPAserviceStudy.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public string Genre { get; set; }

        //public DateTime PublishDate { get; set; }

        //public string Description { get; set; }

        public int AuthorId { get; set; }
        // use virtual for lazing loading
        //public virtual Author Author { get; set; }
        public Author Author { get; set; }
    }
}