using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Display(Name = "имя")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "почта")]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Display(Name = "фамилия")]
        public string SurName { get; set; }

        List<BookModel> books { get; set; } = new List<BookModel>();
    }
}