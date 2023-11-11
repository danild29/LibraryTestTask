using DataLibrary.Data.Services;
using DataLibrary.Models;
using Library.Helpers.Mapping;
using Library.Models;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController()
        {
            _service = ServiceFactory.GetUserService();
        }

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
        public async Task<ActionResult> Create(UserModel model)
        {
            if (ModelState.IsValid)
            {
                User user = MyCustomMapping.MapUserModelToUser(model);
                await _service.InsertUser(user);

                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var user = await _service.GetUser((int)id);
            return View(user);
        }

    }
}