using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class SleepReviewService
    {
        private readonly ISleepReviewRepository _sleepReviewRepository;

        public SleepReviewService(ISleepReviewRepository sleepReviewRepository)
        {
            _sleepReviewRepository = sleepReviewRepository;
        }

        public List<SleepReviewDTO> GetAllSleepReviews()
        {
            return _sleepReviewRepository.GetSleepReviews();
        }

        public List<SleepReviewDTO> GetSleepReviewsByUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("De gebruikers-ID moet groter zijn dan 0.");

            return _sleepReviewRepository.GetSleepReviewsByUserId(userId);
        }

        public bool UpdateReview(SleepReviewDTO review)
        {
            ValidateSleepReview(review);
            return _sleepReviewRepository.UpdateSleepReview(review);
        }

        public bool AddSleepReview(SleepReviewDTO review)
        {
            ValidateSleepReview(review);
            return _sleepReviewRepository.AddSleepReview(review);
        }

        public bool DeleteSleepReview(int reviewId)
        {
            if (reviewId <= 0)
                throw new ArgumentException("De review-ID moet groter zijn dan 0.");

            return _sleepReviewRepository.DeleteSleepReview(reviewId);
        }

        public List<SleepReviewDTO> GetSleepReviewsByFollowedUsers(List<int> followedUserIds)
        {
            if (followedUserIds == null || !followedUserIds.Any())
                throw new ArgumentException("De lijst met gevolgde gebruikers mag niet leeg zijn.");

            return _sleepReviewRepository.GetSleepReviewsByUserIds(followedUserIds);
        }

        private void ValidateSleepReview(SleepReviewDTO review)
        {
            if (review.SleepRating < 1 || review.SleepRating > 10) 
                throw new ArgumentException("De slaapscore moet tussen 1 en 10 liggen.");

            if (string.IsNullOrWhiteSpace(review.Description))
                throw new ArgumentException("De beschrijving mag niet leeg zijn.");

            if (review.Description.Length > 255) 
                throw new ArgumentException("De beschrijving mag niet langer zijn dan 255 tekens.");

            //if (review.StartTime >= review.EndTime)
            //    throw new ArgumentException("De begintijd moet eerder zijn dan de eindtijd.");

            if (review.Date == default)
                throw new ArgumentException("De datum moet geldig zijn.");
        }

    }
}
