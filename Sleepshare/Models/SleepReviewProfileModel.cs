

namespace PresentationLayer.Models
{
    public class SleepReviewProfileModel
    {
        public string Username { get; set; }
        public List<Sleepshare.Models.SleepReview> SleepReviews { get; set; } // Use the correct namespace
    }
}
