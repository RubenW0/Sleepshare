using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class SleepReviewDTO
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public int SleepRating { get; set; }
        public string Description { get; set; }
        public int SleepGoal { get; set; }
        public int SleepDuration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
