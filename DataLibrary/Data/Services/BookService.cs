using DataLibrary.Data.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Data.Services
{
    public class BookService
    {
        private readonly ISqlDataAccess _data;


        public BookService(ISqlDataAccess data)
        {
            _data = data;
        }



        public async Task<Book> GetBook(int id)
        {
            string sql = @"select * From [Books] Where Id = @Id";
            Book book = (await _data.LoadData<Book, dynamic>(sql, new { Id = id })).FirstOrDefault();
            return book;
        }


        public async Task<IEnumerable<Book>> GetBooks()
        {
            string sql = @"SELECT * from [Books]";
            return await _data.LoadData<Book, dynamic>(sql, new { });
        }

        public async Task InsertBook(Book book)
        {
            string sql = @"insert into [Books](Name, Author, Publisher, Year, Genre, ISBN, Status)
                                        values(@Name, @Author, @Publisher, @Year, @Genre, @ISBN, @Status)";
            var parametrs = new { book.Name, book.Author, book.Publisher, book.Year, book.Genre, book.ISBN, book.Status };
            await _data.SaveData(sql, parametrs);
        }


        public async Task UpdateBook(Book book)
        {
            string sql = @"UPDATE [Books] 
                            set Name = IsNull(@Name, Name), 
                            Author= IsNull(@Author, Author),
                            Publisher = IsNull(@Publisher, Publisher),
                            Year= IsNull(@Year, Year),
                            Genre= IsNull(@Genre, Genre)
                            WHERE Id = @Id";

            var parametrs = new { book.Name, book.Author, book.Publisher, book.Year, book.Genre, book.Id };
            await _data.SaveData(sql, parametrs);
        }



        public async Task LoanBook(string firstName, string surName, int bookId, DateTime start, TimeSpan span, string status = "Issued")
        {
            string sql = @"UPDATE [dbo].[Books] set Available = 0 WHERE Id = @Id";
            await _data.SaveData(sql, new { Id = bookId, Status = status });

            string sql1 = @"Insert [dbo].[Loans]( BookId, MemberId, LoanDate, ReturnDate, Status) 
                        values(@BookId, (select top 1 Id from [Users] Where FirstName = @FirstName and SurName = @SurName), @LoanDate, @ReturnDate, @Status)";

            var parametrs = new { BookId = bookId, FirstName = firstName, Surname = surName, LoanDate = start, ReturnDate = start + span, Status = status };
            await _data.SaveData(sql1, parametrs);
        }


        public async Task ReturnBook(int bookId, DateTime returnedAt, string status = "Returned")
        {
            string sql = @"UPDATE [Books] set Available = 1 WHERE Id = @Id";
            await _data.SaveData(sql, new { Id = bookId});

            string sql2 = @"Update [Loans] set Status=@Status, ReturnDate = @ReturnDate WHERE BookId = @BookId";
            await _data.SaveData(sql2, new { BookId = bookId, Status = status, ReturnDate = returnedAt });
        }

        
        
        
        
        public async Task<Loan> GetLoanByBook(int bookId)
        {
            string sql = sql = @"select l.Id as LoanId, u.Id as UserId, b.Id as BookId, l.LoanDate, l.ReturnDate, l.Status,
                           u.Email, u.FirstName, u.SurName,
                           b.[Name], b.Publisher, b.Author
                        From [Loans] as l
                        join [Books] as b on b.Id = l.BookId
                        join [Users] as u on u.Id = l.MemberId
                        Where b.Id = @Id and l.Status != 'Returned'";

            var parametrs = new { Id = bookId };
            
            var book =  await _data.GetLoan(sql, parametrs, "Email, Name");
            return book.FirstOrDefault();
        }
    }
}