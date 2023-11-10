using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class LoanModel
    {

        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; } 
        public string SurName { get; set; }  
    }
}