using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.IRepositorys;

namespace BusinessLogicLayer.Services
{
    public class FollowerService
    {
        private readonly IFollowerRepository _followerRepository;

        public FollowerService(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public void FollowUser(int userId, int followsId)
        {
            _followerRepository.FollowUser(userId, followsId);
        }

        public void UnfollowUser(int userId, int followsId)
        {
            _followerRepository.UnfollowUser(userId, followsId);
        }

        public List<FollowerDTO> GetFollowers(int userId)
        {
            return _followerRepository.GetFollowers(userId);
        }

        public List<int> GetFollowedUserIds(int userId)
        {
            return _followerRepository.GetFollowedUserIds(userId);
        }

        public List<UserDTO> GetAllUsers()
        {
            return _followerRepository.GetAllUsers();
        }

        public bool IsFollowing(int userId, int followsId)
        {
            return _followerRepository.IsFollowing(userId, followsId);
        }

    }
}
