using System.Collections.Generic;

namespace DataLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }


        public List<Loan> Loans { get; set; }
    }
}
