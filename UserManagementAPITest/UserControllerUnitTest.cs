using UserManagementAPI.Controllers;
using UserManagementAPI.Services.Interface;
using Moq;
using UserManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserManagementAPITest;

public class UserControllerUnitTest
{
    private Mock<IUserService> _mockUserService;
    private UserController _controller;
    [SetUp]
    public void Setup()
    {
        _mockUserService = new Mock<IUserService>();

        _controller = new UserController(_mockUserService.Object);
    }

    [Test]
    public void CreateValidUser()
    {
        var newUser = new User { UserId = 1, UserName = "admin@gmail.com", FirstName = "John", LastName = "Smith", DOB = new DateTime(1992, 7, 29), Address = "Dublin, Ireland" };
        _mockUserService.Setup(s => s.AddUser(newUser)).Returns(newUser.UserId);

        var result = _controller.Create(newUser) as ObjectResult;

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void GetAllUsers()
    {
        var mockUsers = new List<User>
            {
                new User { UserId = 1, UserName = "smith@gmail.com", FirstName = "smith", LastName = "craig", DOB = new DateTime(1995, 2, 9), Address = "Dublin, Ireland" },
                new User { UserId = 2, UserName = "sharma@gmail.com", FirstName = "Sharma", LastName = "khan", DOB = new DateTime(1997, 11, 10), Address = "Dublin, Ireland" }
            };
        _mockUserService.Setup(s => s.GetAllUsers()).Returns(mockUsers);

        var result = _controller.GetAll() as OkObjectResult;

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.EqualTo(mockUsers));
    }

    [Test]
    public void GetUserById()
    {
        int userId = 1;
        var mockUser = new User { UserId = 1, UserName = "@gmail.com", FirstName = "Doco", LastName = "Soft", DOB = new DateTime(1996, 9, 23), Address = "Dublin, Ireland" };
        _mockUserService.Setup(s => s.GetUserById(userId)).Returns(mockUser);

        var result = _controller.GetById(userId) as OkObjectResult;

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.EqualTo(mockUser));
    }

    [Test]
    public void UpdateUser()
    {
        var user = new User { UserId = 1, UserName = "kamal@gmail.com", FirstName = "Kamal", LastName = "Sam", DOB = new DateTime(1992, 7, 29), Address = "Ashewood, Loais, Ireland" };
        _mockUserService.Setup(s => s.UpdateUser(user));

        var result = _controller.Update(user.UserId, user) as OkObjectResult;

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void DeleteUser()
    {
        int userId = 1;
        _mockUserService.Setup(s => s.DeleteUser(userId));

        var result = _controller.Delete(userId) as OkObjectResult;

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void CreateInvalidUserName()
    {
        var user = new User { UserId = 1, UserName = "admin.com", FirstName = "Yathi", LastName = "Kamal", DOB = new DateTime(2017, 9, 24), Address = "Limerick, Ireland" };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(user);

        var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

        Assert.That(isValid, Is.False);
        Assert.That(validationResults.Count, Is.GreaterThan(0));
        Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The UserName field is not a valid e-mail address."));
    }

    [Test]
    public void GetUserWithInvalidUserId()
    {
        int userId = -1;
        User? user = null;
        _mockUserService.Setup(s => s.GetUserById(userId)).Returns(user);

        var result = _controller.GetById(userId) as NotFoundObjectResult;

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.EqualTo("User not found."));
    }

    [Test]
    public void GetAllUsersWithNoUsersExist()
    {
        _mockUserService.Setup(s => s.GetAllUsers()).Returns(new List<User>());

        var result = _controller.GetAll() as NotFoundObjectResult;

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.EqualTo("User not found."));
    }

    
    public void DeleteUserWithInvalidUserId()
    {
        int userId = -1;
        _mockUserService.Setup(s => s.DeleteUser(userId));

        var result = _controller.Delete(userId) as NotFoundObjectResult;

        Assert.That(result, Is.Not.Null);
        Assert.That(result.StatusCode, Is.EqualTo(404));
        Assert.That(result.Value, Is.EqualTo("User not found."));
    }
}
