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
            // Validatie: Controleer of de gebruiker zichzelf probeert te volgen
            if (userId == followsId)
            {
                throw new InvalidOperationException("Je kunt jezelf niet volgen.");
            }

            if (_followerRepository.IsFollowing(userId, followsId))
            {
                throw new InvalidOperationException("Je volgt deze gebruiker al.");
            }

            _followerRepository.FollowUser(userId, followsId);
        }

        public void UnfollowUser(int userId, int followsId)
        {
            if (!_followerRepository.IsFollowing(userId, followsId))
            {
                throw new InvalidOperationException("Je volgt deze gebruiker niet.");
            }

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

        public List<UserDTO> GetAllUsers(int userId)
        {
            var allUsers = _followerRepository.GetAllUsers();
            return allUsers.Where(user => user.UserId != userId).ToList();
        }

        public bool IsFollowing(int userId, int followsId)
        {
            return _followerRepository.IsFollowing(userId, followsId);
        }
    }
}