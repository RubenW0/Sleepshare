using Microsoft.AspNetCore.Mvc;
using Sleepshare.Models;
using System.Diagnostics;

namespace Sleepshare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseConnection _databaseService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _databaseService = new DatabaseConnection();
        }

        public IActionResult Index()
        {
            // Get the sleep reviews from the database
            var sleepReviews = _databaseService.GetSleepReviews();
            return View(sleepReviews); // Pass them to the view
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
