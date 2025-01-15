using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;

public class FakeSleepReviewRepository : ISleepReviewRepository
{
    private List<SleepReviewDTO> _sleepReviews = new List<SleepReviewDTO>();

    public List<SleepReviewDTO> GetSleepReviews()
    {
        return _sleepReviews;
    }

    public List<SleepReviewDTO> GetSleepReviewsByUserId(int userId)
    {
        return _sleepReviews.Where(r => r.UserId == userId).ToList();
    }

    public bool AddSleepReview(SleepReviewDTO review)
    {
        _sleepReviews.Add(review);
        return true;
    }

    public bool UpdateSleepReview(SleepReviewDTO review)
    {
        var existingReview = _sleepReviews.FirstOrDefault(r => r.Id == review.Id);
        if (existingReview == null) return false;

        existingReview.SleepRating = review.SleepRating;
        existingReview.Description = review.Description;
        existingReview.SleepGoal = review.SleepGoal;
        existingReview.SleepDuration = review.SleepDuration;
        existingReview.StartTime = review.StartTime;
        existingReview.EndTime = review.EndTime;
        existingReview.Date = review.Date;
        return true;
    }

    public bool DeleteSleepReview(int reviewId)
    {
        var review = _sleepReviews.FirstOrDefault(r => r.Id == reviewId);
        if (review == null) return false;

        _sleepReviews.Remove(review);
        return true;
    }

    public List<SleepReviewDTO> GetSleepReviewsByUserIds(List<int> userIds)
    {
        return _sleepReviews.Where(r => userIds.Contains(r.UserId)).ToList();
    }
}
