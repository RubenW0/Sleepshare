using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Sleepshare.Models;
using System.Diagnostics;
using PresentationLayer.Helpers;

namespace Sleepshare.Controllers
{
    public class HomeController : Controller
    {
        private readonly SleepReviewService _sleepReviewService;

        public HomeController(SleepReviewService sleepReviewService)
        {
            _sleepReviewService = sleepReviewService;
        }

        public IActionResult Index()
        {
            return View();
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
