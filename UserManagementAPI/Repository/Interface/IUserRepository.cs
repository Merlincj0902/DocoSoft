using UserManagementAPI.Models;

namespace UserManagementAPI.Repository.Interface;

public interface IUserRepository 
{
    IList<User> GetAllUsers();
    User? GetUserById(int id);
    User? GetUserByUserName(string userName);
    int AddUser(User user);
    void UpdateUser(User updatedUser);
    void DeleteUser(int id);
}