using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Data.Services
{
    public interface IBookService
    {
        Task<Book> GetBook(int id);
        Task<IEnumerable<Book>> GetBooks();
        Task<Loan> GetLoanByBook(int bookId);
        Task InsertBook(Book book);
        Task LoanBook(string firstName, string surName, int bookId, DateTime start, DateTime end, string status = "Issued");
        Task ReturnBook(int bookId, DateTime returnedAt, string status = "Returned");
        Task UpdateBook(Book book);
    }
}