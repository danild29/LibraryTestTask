using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Library.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Display(Name = "Название книги")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Display(Name = "Автор")]
        public string Author { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Display(Name = "Издатель")]
        public string Publisher { get; set; }
        [Required]
        [Range(0, 2023)]
        [Display(Name = "год издания")]
        public int Year { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Display(Name = "жанр книги")]
        public string Genre { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        public bool Available { get; set; }
        [MaxLength(100)]
        [Display(Name = "состояние книги")]
        public string Status { get; set; }
        
    }
}