using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.IRepositorys;
using System;
using System.Collections.Generic;

public class FakeFollowerRepository : IFollowerRepository
{
    private readonly List<FollowerDTO> _followers = new List<FollowerDTO>();
    private readonly List<int> _followedUserIds = new List<int>();

    public void FollowUser(int userId, int followsId)
    {
        if (IsFollowing(userId, followsId))
        {
            throw new InvalidOperationException("User is already following.");
        }

        _followers.Add(new FollowerDTO { UserId = userId, FollowsId = followsId });
        _followedUserIds.Add(followsId);
    }

    public void UnfollowUser(int userId, int followsId)
    {
        if (!IsFollowing(userId, followsId))
        {
            throw new InvalidOperationException("User is not following.");
        }

        _followers.RemoveAll(f => f.UserId == userId && f.FollowsId == followsId);
        _followedUserIds.Remove(followsId);
    }

    public List<FollowerDTO> GetFollowers(int userId)
    {
        return _followers.FindAll(f => f.UserId == userId);
    }

    public List<int> GetFollowedUserIds(int userId)
    {
        return _followedUserIds;
    }

    public List<UserDTO> GetAllUsers()
    {
        // This is just a dummy list for the test. In real code, it would come from the DB.
        return new List<UserDTO>
        {
            new UserDTO { UserId = 1, Username = "User1" },
            new UserDTO { UserId = 2, Username = "User2" },
            new UserDTO { UserId = 3, Username = "User3" }
        };
    }

    public bool IsFollowing(int userId, int followsId)
    {
        return _followers.Exists(f => f.UserId == userId && f.FollowsId == followsId);
    }
}
