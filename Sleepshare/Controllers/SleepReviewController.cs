using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Helpers;
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
        var sleepReview = SleepReviewMapper.ToModel(sleepReviewDTO);

        return View(sleepReview); 
    }

    // POST Review
    [HttpPost]
    public IActionResult EditReview(SleepReview sleepReview)
    {
        //if (ModelState.IsValid)
        //{
        // Gebruik SleepReviewMapper om het model om te zetten naar een DTO
        var sleepReviewDTO = SleepReviewMapper.ToDTO(sleepReview);

        bool isUpdated = _sleepReviewService.UpdateReview(sleepReviewDTO);

        if (isUpdated)
        {
            return RedirectToAction("Index", "Home");
        }
        //}

        return View(sleepReview);
    }

    // GET: SleepReview/AddReview
    public IActionResult AddReview()
    {
        return View("AddReview");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddSleepReview(SleepReview sleepReview)
    {
        //if (ModelState.IsValid)
        {
            var sleepReviewDTO = SleepReviewMapper.ToDTO(sleepReview);

            bool isAdded = _sleepReviewService.AddSleepReview(sleepReviewDTO);

            if (isAdded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "An error occurred while adding the sleep review.");
            }
        }

        return View(sleepReview);
    }

}
