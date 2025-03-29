using UserManagementAPI.Models;
using UserManagementAPI.Repository.Interface;
using UserManagementAPI.Services.Interface;

namespace UserManagementAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public int AddUser(User user)
    {
        var existingUser = _repo.GetUserById(user.UserId);
        if (existingUser != null)
            throw new Exception("User Id already exists");

        existingUser = _repo.GetUserByUserName(user.UserName);
        if (existingUser != null)
            throw new Exception("User Name already exists");

        return _repo.AddUser(user);
    }

    public void DeleteUser(int id)
    {
        _repo.DeleteUser(id);
    }

    public IList<User> GetAllUsers()
    {
        return _repo.GetAllUsers();
    }

    public User? GetUserById(int id)
    {
        return _repo.GetUserById(id);
    }

    public void UpdateUser(User user)
    {
        var validateUser = _repo.GetUserByUserName(user.UserName);
        if (validateUser != null && validateUser.UserId != user.UserId)
            throw new Exception("User Name already exists");

        _repo.UpdateUser(user);
    }
}