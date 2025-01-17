using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Tests
{
    [TestClass]
    public class FollowerServiceTests
    {
        private FollowerService _followerService;
        private FakeFollowerRepository _fakeFollowerRepository;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the fake repository and the service
            _fakeFollowerRepository = new FakeFollowerRepository();
            _followerService = new FollowerService(_fakeFollowerRepository);
        }

        [TestMethod]
        public void FollowUser_ShouldAddFollower_WhenValid()
        {
            // Arrange
            int userId = 1;
            int followsId = 2;

            // Act
            _followerService.FollowUser(userId, followsId);

            // Assert
            Assert.IsTrue(_followerService.IsFollowing(userId, followsId));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Je volgt deze gebruiker al.")]
        public void FollowUser_ShouldThrowException_WhenAlreadyFollowing()
        {
            // Arrange
            int userId = 1;
            int followsId = 2;
            _followerService.FollowUser(userId, followsId);

            // Act
            _followerService.FollowUser(userId, followsId); // This should throw an exception
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Je kunt jezelf niet volgen.")]
        public void FollowUser_ShouldThrowException_WhenUserTriesToFollowHimself()
        {
            // Arrange
            int userId = 1;
            int followsId = 1;

            // Act
            _followerService.FollowUser(userId, followsId); // This should throw an exception
        }

        [TestMethod]
        public void UnfollowUser_ShouldRemoveFollower_WhenValid()
        {
            // Arrange
            int userId = 1;
            int followsId = 2;
            _followerService.FollowUser(userId, followsId);

            // Act
            _followerService.UnfollowUser(userId, followsId);

            // Assert
            Assert.IsFalse(_followerService.IsFollowing(userId, followsId));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Je volgt deze gebruiker niet.")]
        public void UnfollowUser_ShouldThrowException_WhenNotFollowing()
        {
            // Arrange
            int userId = 1;
            int followsId = 2;

            // Act
            _followerService.UnfollowUser(userId, followsId); // This should throw an exception
        }

        [TestMethod]
        public void GetFollowers_ShouldReturnListOfFollowers()
        {
            // Arrange
            int userId = 1;
            int followsId = 2;
            _followerService.FollowUser(userId, followsId);

            // Act
            var followers = _followerService.GetFollowers(userId);

            // Assert
            Assert.AreEqual(1, followers.Count);
            Assert.AreEqual(followsId, followers[0].FollowsId);
        }

    }
}
