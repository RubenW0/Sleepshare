using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Sleepshare.Models;
using System.Diagnostics;

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
            var sleepReviews = sleepReviewDTOs.Select(dto => new Sleepshare.Models.SleepReview
            {
                Id = dto.Id,
                Reviewer = dto.Reviewer,  // assuming Reviewer exists in your DTO, or map appropriately
                SleepRating = dto.SleepRating,
                Description = dto.Description,
                SleepGoal = dto.SleepGoal,
                SleepDuration = dto.SleepDuration,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Date = dto.Date
            }).ToList();

            return View(sleepReviews);  // Pass the converted list of SleepReview models
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
