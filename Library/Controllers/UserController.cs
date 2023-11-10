using DataLibrary.Data.Services;
using DataLibrary.Models;
using Library.Models;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _service;

        public UserController()
        {
            _service = ServiceFactory.GetUserService();
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            var list = await _service.GetUsers();

            return View(list);
        }


        public ActionResult Create()
        {
            ViewBag.Message = "Регистрация нового пользователя";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                User model = new User
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    SurName = user.SurName,
                };

                await _service.InsertUser(model);

                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _service.GetUser((int)id);
            return View(user);
        }

    }
}