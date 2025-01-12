using Sleepshare.Models;

namespace PresentationLayer.Models
{
    public class ProfileViewModel
    {
        public string Username { get; set; }
        public List<SleepReview> SleepReviews { get; set; } = new List<SleepReview>();
    }
}