using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.IRepositorys;

namespace BusinessLogicLayer.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDTO Login(string username, string password)
        {
            return _userRepository.GetUser (username, password);
        }

        public bool Register(string username, string password)
        {
            if (_userRepository.UserExists(username))
            {
                return false; 
            }

            return _userRepository.AddUser(username, password);
        }


    }
}
