using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.IRepositorys;

namespace BusinessLogicLayerTest.MockRepositorys
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly List<UserDTO> _users;

        public FakeUserRepository()
        {
            _users = new List<UserDTO>
            {
                new UserDTO { Username = "testuser1", Password = "Password1" }
            };
        }

        public UserDTO GetUser(string username, string password)
        {
            return _users.FirstOrDefault(user => user.Username == username && user.Password == password);
        }

        public bool AddUser(string username, string password)
        {
            if (_users.Any(user => user.Username == username))
            {
                return false;
            }

            _users.Add(new UserDTO { Username = username, Password = password });
            return true;
        }

        public bool UserExists(string username)
        {
            return _users.Any(user => user.Username == username);
        }
    }
}
