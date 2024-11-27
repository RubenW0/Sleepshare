using System.ComponentModel.DataAnnotations;

namespace Sleepshare.Models
{
    public class SleepReview
    {
        public int Id { get; set; }             
        public string Reviewer { get; set; }   
        public int SleepRating { get; set; }
        [Required]
        public string Description { get; set; }  
        public int SleepGoal { get; set; }
        public int SleepDuration { get; set; }    
        public DateTime StartTime { get; set; }  
        public DateTime EndTime { get; set; }  
        public DateTime Date { get; set; }  
    }
}
