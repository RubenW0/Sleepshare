using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Sleepshare.Models;

public class SleepReviewController : Controller
{
    private readonly SleepReviewService _sleepReviewService;


    public SleepReviewController(IConfiguration configuration)
    {
        _sleepReviewService = new SleepReviewService(new SleepReviewRepository(configuration));
    }

    // GET Review
    public IActionResult EditReview(int id)
    {
        var sleepReviewDTO = _sleepReviewService.GetAllSleepReviews()
            .FirstOrDefault(s => s.Id == id); 

        if (sleepReviewDTO == null)
        {
            return NotFound();
        }

        // Convert DTO to SleepReviewModel
        var sleepReview = new Sleepshare.Models.SleepReview
        {
            Id = sleepReviewDTO.Id,
            SleepRating = sleepReviewDTO.SleepRating,
            Description = sleepReviewDTO.Description,
            SleepGoal = sleepReviewDTO.SleepGoal,
            SleepDuration = sleepReviewDTO.SleepDuration,
            StartTime = sleepReviewDTO.StartTime,
            EndTime = sleepReviewDTO.EndTime,
            Date = sleepReviewDTO.Date
        };

        return View(sleepReview); 
    }

    // POST Review
    [HttpPost]
    public IActionResult EditReview(SleepReview sleepReview)
    {
        //if (ModelState.IsValid)
        //{
            bool isUpdated = _sleepReviewService.UpdateReview(new SleepReviewDTO
            {
                Id = sleepReview.Id,
                SleepRating = sleepReview.SleepRating,
                Description = sleepReview.Description,
                SleepGoal = sleepReview.SleepGoal,
                SleepDuration = sleepReview.SleepDuration,
                StartTime = sleepReview.StartTime,
                EndTime = sleepReview.EndTime,
                Date = sleepReview.Date
            });
            if (isUpdated)
            {
                return RedirectToAction("Index", "Home");
            }
       //}

        return View(sleepReview);
    }


}
