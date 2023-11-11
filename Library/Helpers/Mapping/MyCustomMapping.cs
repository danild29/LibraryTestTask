using DataLibrary.Models;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Helpers.Mapping
{
    public static class MyCustomMapping
    {

        public static User MapUserModelToUser(UserModel model)
        {
            User user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                SurName = model.SurName,
            };
            return user;
        }


        public static Book MapBookModelToBook(BookModel model)
        {
            Book book = new Book
            {
                Name = model.Name,
                Author = model.Author,
                Publisher = model.Publisher,
                Year = model.Year,
                Genre = model.Genre,
                ISBN = model.ISBN,
                Available = true,
                Status = "good"
            };
            return book;
        }


        public static BookModel MapBookToBookModel(Book book)
        {
            BookModel model = new BookModel()
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Publisher = book.Publisher,
                Year = book.Year,
                Genre = book.Genre,
                ISBN = book.ISBN,
                Available = book.Available,
                Status = book.Status
            };
            return model;
        }

        public static LoanModel MapLoanToLoanModel(Loan loan)
        {
            var model = new LoanModel
            {
                UserId = loan.UserId,
                BookId = loan.BookId,
                Start = loan.LoanDate,
                End = loan.ReturnDate,
                Status = loan.Status,
                FirstName = loan.User.FirstName,
                SurName = loan.User.SurName,
                Name = loan.Book.Name,
            };
            return model;
        }
    }
}