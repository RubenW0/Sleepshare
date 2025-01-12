using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.IRepositorys
{
    public interface IUserRepository
    {
        UserDTO GetUser(string username, string password);
        bool AddUser(string username, string password);
        bool UserExists(string username);



        //bool CreateUser(UserDTO user);
        //UserDTO GetUserById(int userId);
        //List<UserDTO> GetAllUsers();
    }
}
