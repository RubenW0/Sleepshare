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

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Index()
        {
            var model = new LoginViewModel(); // Ensure the model is not null
            return View(model); // Pass the model to the view
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to find the user with the provided credentials
                var foundUser = _userService.Login(model.Username, model.Password);

                if (foundUser != null)
                {
                    // User found, redirect to the home page or user dashboard
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Invalid credentials, add an error to the ModelState
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we reach here, something went wrong. Return the same model back to the view
            return View(model);
        }


    }
}
