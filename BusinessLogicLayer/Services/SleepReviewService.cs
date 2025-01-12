using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _sleepReviewRepository.GetSleepReviewsByUserId(userId);
        }

        public bool UpdateReview(SleepReviewDTO review)
        {
            return _sleepReviewRepository.UpdateSleepReview(review);
        }

        public bool AddSleepReview(SleepReviewDTO review)
        {
            return _sleepReviewRepository.AddSleepReview(review);
        }

        public bool DeleteSleepReview(int reviewId)
        {
            return _sleepReviewRepository.DeleteSleepReview(reviewId);
        }

        public List<SleepReviewDTO> GetSleepReviewsByFollowedUsers(List<int> followedUserIds)
        {
            return _sleepReviewRepository.GetSleepReviewsByUserIds(followedUserIds);
        }


    }
}
