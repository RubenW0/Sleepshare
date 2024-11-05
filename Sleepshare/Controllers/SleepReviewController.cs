using Microsoft.AspNetCore.Mvc;
using Sleepshare.Models;

public class SleepReviewController : Controller
{
    private readonly DatabaseConnection _databaseService;

    public SleepReviewController()
    {
        _databaseService = new DatabaseConnection();
    }

    // GET Review
    public IActionResult EditReview(int id)
    {
        var sleepReview = _databaseService.GetSleepReviews().FirstOrDefault(r => r.Id == id);
        if (sleepReview == null)
        {
            return NotFound(); 
        }
        return View(sleepReview);
    }

    // POST Review
    [HttpPost]
    public IActionResult EditReview(SleepReview sleepReview)
    {
        //if (ModelState.IsValid)
        //{
            bool isUpdated = _databaseService.UpdateSleepReview(
                sleepReview.Id,
                sleepReview.SleepRating,
                sleepReview.Description,
                sleepReview.SleepGoal,
                sleepReview.SleepDuration,
                sleepReview.StartTime,
                sleepReview.EndTime,
                sleepReview.Date
            );

            if (isUpdated)
            {
                return RedirectToAction("Index", "Home");
            }
        //}

        return View(sleepReview);
    }


}
