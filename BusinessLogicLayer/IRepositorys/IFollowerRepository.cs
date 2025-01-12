using BusinessLogicLayer.DTOs;
using System.Collections.Generic;

namespace BusinessLogicLayer.IRepositorys
{
    public interface IFollowerRepository
    {
        void FollowUser(int userId, int followsId);
        void UnfollowUser(int userId, int followsId);
        List<FollowerDTO> GetFollowers(int userId);
        List<int> GetFollowedUserIds(int userId);
        List<UserDTO> GetAllUsers();
        bool IsFollowing(int userId, int followsId);
    }
}
