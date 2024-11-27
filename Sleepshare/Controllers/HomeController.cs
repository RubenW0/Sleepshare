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

        public HomeController(IConfiguration configuration)
        {
            _sleepReviewService = new SleepReviewService(new SleepReviewRepository(configuration));
        }

        public IActionResult Index()
        {
            var sleepReviewDTOs = _sleepReviewService.GetAllSleepReviews();

            var sleepReviews = sleepReviewDTOs
                .Select(SleepReviewMapper.ToModel)
                .ToList();

            return View(sleepReviews);
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
