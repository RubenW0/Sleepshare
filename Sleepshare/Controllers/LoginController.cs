using BusinessLogicLayer.IRepositorys;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;

        public LoginController(IConfiguration configuration)
        {
            _userService = new UserService(new UserRepository(configuration));
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var foundUser = _userService.Login(model.Username, model.Password);

                if (foundUser != null)
                {
                    HttpContext.Session.SetString("Username", foundUser.Username);
                    HttpContext.Session.SetInt32("UserId", foundUser.Id);
                    return RedirectToAction("Profile", "Login"); 
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password."); 
                }
            }

            return View("Index", model); 
        }



        [HttpGet]
        public IActionResult Account()
        {

            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }

            var model = new ProfileViewModel
            {
                Username = username
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Login");
        }
    }
}
