using UserManagementAPI.Models;

namespace UserManagementAPI.Services.Interface;

public interface IUserService
{
    IList<User> GetAllUsers();
    User? GetUserById(int id);
    int AddUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int id);
}