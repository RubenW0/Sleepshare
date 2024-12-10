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
            // Check if the user is logged in by checking for a session value
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                // If not logged in, redirect to the login page
                return RedirectToAction("Login");
            }

            // If logged in, redirect to the Profile page
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            // Retrieve the username from the session
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                // If the session is empty (user not logged in), redirect to the login page
                return RedirectToAction("Login");
            }

            // Pass the username to the profile view
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
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Index", "Login"); // Redirect to login page
        }
    }
}
