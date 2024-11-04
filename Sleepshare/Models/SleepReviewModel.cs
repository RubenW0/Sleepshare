namespace Sleepshare.Models
{
    public class SleepReview
    {
        public int Id { get; set; }               // De primaire sleutel (id)
        public string Reviewer { get; set; }       // Gebruikersnaam (username uit users table, opgehaald met JOIN)
        public int SleepRating { get; set; }       // Slaap beoordeling (sleep_rating)
        public string Description { get; set; }    // Beschrijving (description)
        public int SleepGoal { get; set; }         // Slaap doel (sleep_goal)
        public int SleepDuration { get; set; }     // Slaap duur in minuten (sleep_duration)
        public DateTime StartTime { get; set; }    // Starttijd van slaap (start_time)
        public DateTime EndTime { get; set; }      // Eindtijd van slaap (end_time)
        public DateTime Date { get; set; }         // Datum van de review (date)
    }
}
