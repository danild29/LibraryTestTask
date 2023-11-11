using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Library.Models
{
    public class LoanModel
    {

        public int UserId { get; set; }
        public int BookId { get; set; }
        
        [Display(Name = "название книги")]
        public string Name { get; set; }
        [Display(Name = "дата заёма книги")]
        public DateTime Start { get; set; }

        [Display(Name = "дата возрата книги")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; } 

        [Display(Name = "состояние заема")]
        public string Status { get; set; }
        

        [Required]
        [Display(Name = "имя заемщика")]
        [MaxLength(100)]
        [MinLength(1)]
        public string FirstName { get; set; } 
        [Display(Name = "фамилия заемщика")]
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string SurName { get; set; }  
    }
}