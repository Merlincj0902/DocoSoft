using Moq;
using UserManagementAPI.Models;
using UserManagementAPI.Services;
using UserManagementAPI.Repository.Interface;

namespace UserManagementAPITest;

public class UserServiceUnitTest
{
    private Mock<IUserRepository> _mockUserRepository;
    private UserService _userService;
    
    [SetUp]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();

        _userService = new UserService(_mockUserRepository.Object);
    }

    [Test]
    public void UpdateUserWithExistingUserName()
    {
        var existingUser = new User { UserId = 2, UserName = "admin@gmail.com", FirstName = "Joe", LastName = "Andrew", DOB = new DateTime(1992, 7, 29), Address = "Dublin, Ireland" };
        _mockUserRepository.Setup(r => r.GetUserByUserName(existingUser.UserName)).Returns(existingUser);

        var updateUser = new User { UserId = 1, UserName = "admin@gmail.com", FirstName = "Andrew", LastName = "Crowe", DOB = new DateTime(1995, 2, 9), Address = "Dublin, Ireland" };

        var exception = Assert.Throws<Exception>(() => _userService.UpdateUser(updateUser));

        Assert.That(exception.Message, Is.EqualTo("User Name already exists"));
    }

    [Test]
    public void CreateUserWithUserIdAlreadyExists()
    {
        var existingUser = new User { UserId = 1, UserName = "admin@gmail.com", FirstName = "Arun", LastName = "Ram", DOB = new DateTime(1997, 11, 10), Address = "Dublin, Ireland" };
        _mockUserRepository.Setup(r => r.GetUserById(existingUser.UserId)).Returns(existingUser);

        var newUser = new User { UserId = 1, UserName = "admin@gmail.com", FirstName = "Katy", LastName = "joe", DOB = new DateTime(1969, 9, 18), Address = "Dublin, Ireland" };

        var ex = Assert.Throws<Exception>(() => _userService.AddUser(newUser));

        Assert.That(ex.Message, Is.EqualTo("User Id already exists"));
    }

    [Test]
    public void CreateUserWithUserNameAlreadyExists()
    {
        var existingUser = new User { UserId = 1, UserName = "smith@gmail.com", FirstName = "Smith", LastName = "Sharma", DOB = new DateTime(1953, 4, 22), Address = "Dublin, Ireland" };
        _mockUserRepository.Setup(r => r.GetUserByUserName(existingUser.UserName)).Returns(existingUser);

        var newUser = new User { UserId = 2, UserName = "smith@gmail.com", FirstName = "Sharma", LastName = "Khan", DOB = new DateTime(1992, 7, 29), Address = "Dublin, Ireland" };

        var ex = Assert.Throws<Exception>(() => _userService.AddUser(newUser));

        Assert.That(ex.Message, Is.EqualTo("User Name already exists"));
    }
}
