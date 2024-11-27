using BusinessLogicLayer.DTOs;
using Sleepshare.Models;

namespace PresentationLayer.Helpers
{
    public static class SleepReviewMapper
    {
        public static SleepReview ToModel(SleepReviewDTO dto)
        {
            return new SleepReview
            {
                Id = dto.Id,
                Reviewer = dto.Reviewer,
                SleepRating = dto.SleepRating,
                Description = dto.Description,
                SleepGoal = dto.SleepGoal,
                SleepDuration = dto.SleepDuration,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Date = dto.Date
            };
        }

        public static SleepReviewDTO ToDTO(SleepReview model)
        {
            return new SleepReviewDTO
            {
                Id = model.Id,
                Reviewer = model.Reviewer,
                SleepRating = model.SleepRating,
                Description = model.Description,
                SleepGoal = model.SleepGoal,
                SleepDuration = model.SleepDuration,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Date = model.Date
            };
        }
    }
}