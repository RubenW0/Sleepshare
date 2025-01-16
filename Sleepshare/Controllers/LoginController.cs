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

        public LoginController(UserService userService)
        {
            _userService = userService;
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
                    HttpContext.Session.SetInt32("UserId", foundUser.UserId);

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

        [HttpGet]
        public IActionResult NotLoggedIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(model);
                }

                var isRegistered = _userService.Register(model.Username, model.Password);

                if (isRegistered)
                {
                    TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError("", "Username is already taken.");
                }
            }

            return View(model);
        }


    }
}
