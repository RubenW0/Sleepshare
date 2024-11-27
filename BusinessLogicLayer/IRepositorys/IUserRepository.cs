using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.IRepositorys
{
    public interface IUserRepository
    {
        UserDTO GetUser(string username, string password);
        //bool CreateUser(UserDTO user);
        //UserDTO GetUserById(int userId);
        //List<UserDTO> GetAllUsers();
    }
}
