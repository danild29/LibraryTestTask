using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public bool Available { get; set; }
        public string Status { get; set; }


        public User Owner { get; set; }
    }
}
