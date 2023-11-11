using DataLibrary.Data.Services;
using DataLibrary.Models;
using Library.Data.Services;
using Library.Helpers.Mapping;
using Library.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _service;

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
        public async Task<ActionResult> Create(BookModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = MyCustomMapping.MapBookModelToBook(model);
                await _service.InsertBook(book);
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
            if (id == null) return RedirectToAction("Index");

            Book book = await _service.GetBook((int)id);
            if (book == null) return HttpNotFound();

            BookModel model = MyCustomMapping.MapBookToBookModel(book);
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Book book)
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
            LoanModel model = await GetLoanModel(book.Available, book.Id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Loans(LoanModel loan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.LoanBook(loan.FirstName, loan.SurName, loan.BookId, DateTime.Now, loan.End);
                    return RedirectToAction("Index");

                }
                return View(loan);
            }
            catch (Exception)
            {
                ViewBag.Error = "Такого клиента нету";
                return View(loan);
            }
        }

        public async Task<ActionResult> Return(int? bookId)
        {
            await _service.ReturnBook((int)bookId, DateTime.UtcNow);

            return RedirectToAction("Index");
        }




        // private method helpers

        private async Task<LoanModel> GetLoanModel(bool available, int id)
        {
            LoanModel model;

            if (!available)
            {
                Loan loan = await _service.GetLoanByBook(id);
                model = MyCustomMapping.MapLoanToLoanModel(loan);
            }
            else
            {
                Book singleBook = await _service.GetBook(id);
                model = new LoanModel()
                {
                    BookId = singleBook.Id,
                    Name = singleBook.Name,
                    UserId = 0,
                };
            }
            return model;
        }
    }
}