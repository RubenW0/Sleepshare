using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogicLayerTest.ServiceTests
{
    [TestClass]
    public class SleepReviewServiceTest
    {
        private readonly SleepReviewService _service;
        private readonly FakeSleepReviewRepository _repository;

        public SleepReviewServiceTest()
        {
            _repository = new FakeSleepReviewRepository();
            _service = new SleepReviewService(_repository);
        }

        [TestMethod]
        public void GetAllSleepReviews_ShouldReturnAllReviews()
        {
            // Arrange
            var review1 = new SleepReviewDTO { Id = 1, UserId = 1, SleepRating = 8 };
            var review2 = new SleepReviewDTO { Id = 2, UserId = 2, SleepRating = 7 };
            _repository.AddSleepReview(review1);
            _repository.AddSleepReview(review2);

            // Act
            var result = _service.GetAllSleepReviews();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void AddSleepReview_ShouldAddReview()
        {
            // Arrange
            var review = new SleepReviewDTO
            {
                Id = 1,
                UserId = 1,
                SleepRating = 9,
                Description = "Great sleep!",
                SleepGoal = 8,
                SleepDuration = 7,
                StartTime = DateTime.Now.AddHours(-7),
                EndTime = DateTime.Now,
                Date = DateTime.Today
            };

            // Act
            var result = _service.AddSleepReview(review);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _repository.GetSleepReviews().Count);
        }

        [TestMethod]
        public void UpdateSleepReview_ShouldUpdateExistingReview()
        {
            // Arrange
            var review = new SleepReviewDTO
            {
                Id = 1,
                UserId = 1,
                SleepRating = 8,
                Description = "Initial description",
                SleepGoal = 8,
                SleepDuration = 7,
                StartTime = DateTime.Now.AddHours(-7),
                EndTime = DateTime.Now,
                Date = DateTime.Today
            };
            _repository.AddSleepReview(review);

            var updatedReview = new SleepReviewDTO
            {
                Id = 1,
                UserId = 1,
                SleepRating = 10,
                Description = "Updated description",
                SleepGoal = 8,
                SleepDuration = 7,
                StartTime = DateTime.Now.AddHours(-7),
                EndTime = DateTime.Now,
                Date = DateTime.Today
            };

            // Act
            var result = _service.UpdateReview(updatedReview);

            // Assert
            Assert.IsTrue(result);
            var updatedReviewFromRepo = _repository.GetSleepReviews().Find(r => r.Id == 1);
            Assert.IsNotNull(updatedReviewFromRepo, "Updated review should not be null");
            Assert.AreEqual(10, updatedReviewFromRepo.SleepRating);
            Assert.AreEqual("Updated description", updatedReviewFromRepo.Description);
        }

        [TestMethod]
        public void DeleteSleepReview_ShouldRemoveReview()
        {
            // Arrange
            var review = new SleepReviewDTO { Id = 1, UserId = 1, SleepRating = 8 };
            _repository.AddSleepReview(review);

            // Act
            var result = _service.DeleteSleepReview(1);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _repository.GetSleepReviews().Count);
        }

        [TestMethod]
        public void GetSleepReviewsByUserId_ShouldReturnCorrectReviews()
        {
            // Arrange
            var review1 = new SleepReviewDTO { Id = 1, UserId = 1, SleepRating = 8 };
            var review2 = new SleepReviewDTO { Id = 2, UserId = 2, SleepRating = 7 };
            _repository.AddSleepReview(review1);
            _repository.AddSleepReview(review2);

            // Act
            var result = _service.GetSleepReviewsByUserId(1);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(8, result[0].SleepRating);
        }
    }
}
