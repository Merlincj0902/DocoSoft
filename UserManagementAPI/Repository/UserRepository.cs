using UserManagementAPI.Configuration;
using UserManagementAPI.Models;
using UserManagementAPI.Repository.Interface;

namespace UserManagementAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public IList<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public User? GetUserById(int id)
    {
        return _context.Users.Find(id);
    }

    public User? GetUserByUserName(string userName)
    {
        return _context.Users.FirstOrDefault(x => x.UserName == userName);
    }

    public int AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user.UserId;
    }

    public void UpdateUser(User updatedUser)
    {
        var existingUser = _context.Users.Find(updatedUser.UserId);
        if (existingUser == null)
            throw new Exception("User not found.");

        existingUser.UserName = updatedUser.UserName;
        existingUser.FirstName = updatedUser.FirstName;
        existingUser.LastName = updatedUser.LastName;
        existingUser.Address = updatedUser.Address;
        existingUser.DOB = updatedUser.DOB;
        existingUser.ModifiedBy = "admin@gmail.com";
        existingUser.ModifiedDate = DateTime.Now;

        _context.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == id);

        if (user == null)
            throw new Exception("User not found.");

        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}