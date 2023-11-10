using DataLibrary.Data.Services;
using DataLibrary.Models;
using Library.Data.Services;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _service;

        public BookController()
        {
            _service = ServiceFactory.GetBookService();
        }

        public async Task<ActionResult> Index()
        {
            var books = await _service.GetBooks();
            return View(books);
        }




        public ActionResult Create()
        {
            ViewBag.Message = "Регистрация нового пользователя";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookModel book)
        {
            if (ModelState.IsValid)
            {
                Book model = new Book
                {
                    Name = book.Name,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Year = book.Year,
                    Genre = book.Genre,
                    ISBN = book.ISBN,
                    Available = true,
                    Status = book.Status
                };

                await _service.InsertBook(model);

                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<ActionResult> Details(int Id)
        {
            var book = await _service.GetBook(Id);

            return View(book);
        }



        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await _service.GetBook((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Book book) //[Bind(Include = "Id,Name,Author,Publisher,Year")]
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public async Task<ActionResult> Loans(Book book)
        {
            if (book.Id == 0) return RedirectToAction("Index");


            if(!book.Available)
            {
                Loan loan = await _service.GetLoanByBook(book.Id);

                LoanModel model = new LoanModel
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

                return View(model);
            }
            else
            {
                Book singleBook = await _service.GetBook(book.Id);
                LoanModel model = new LoanModel()
                {
                    BookId = singleBook.Id,
                    Name = singleBook.Name,
                    UserId = 0,
                };

                return View(model);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Loans(LoanModel loan)
        {
            await _service.LoanBook(loan.FirstName, loan.SurName, loan.BookId, DateTime.Now, TimeSpan.FromDays(7));
            return View(loan);
        }

        public async Task<ActionResult> Return(int? bookId)
        {
            await _service.ReturnBook((int)bookId, DateTime.UtcNow);

            return RedirectToAction("Index");
        }


    }
}