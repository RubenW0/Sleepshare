using Microsoft.AspNetCore.Mvc;
using Sleepshare.Models;

public class SleepReviewController : Controller
{
    private readonly DatabaseConnection _databaseService;

    public SleepReviewController()
    {
        _databaseService = new DatabaseConnection();
    }

    // GET: SleepReview/EditReview/5
    public IActionResult EditReview(int id)
    {
        var sleepReview = _databaseService.GetSleepReviews().FirstOrDefault(r => r.Id == id);
        if (sleepReview == null)
        {
            return NotFound(); // Return a 404 if the review is not found
        }
        return View(sleepReview); // Pass the specific review to the view
    }

    [HttpPost]
    public IActionResult EditReview(SleepReview sleepReview)
    {
        if (ModelState.IsValid)
        {
            // Roep de update-methode aan en geef de juiste parameters door
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
                // Redirect naar de index of een andere actie na succesvol bijwerken
                return RedirectToAction("Index");
            }
        }

        return View(sleepReview); // Als het model niet geldig is, geef de view opnieuw weer met fouten
    }


}
