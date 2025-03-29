using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services.Interface;
namespace UserManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("ListAllUsers")]
    public IActionResult GetAll()
    {
        var users = _service.GetAllUsers();

        if (users.Count == 0)
            return NotFound("User not found.");

        return Ok(users);
    }

    [HttpGet("User/{id}")]
    public IActionResult GetById(int id)
    {
        var user = _service.GetUserById(id);

        if (user == null)
            return NotFound("User not found.");

        return Ok(user);
    }

    [HttpPost("AddUser")]
    public IActionResult Create([FromBody] User newUser)
    {
        int id = _service.AddUser(newUser);
        return CreatedAtAction(nameof(GetById), new { id = id }, newUser);
    }

    [HttpPut("UpdateUser/{id}")]
    public IActionResult Update(int id, [FromBody] User updatedUser)
    {
        _service.UpdateUser(updatedUser);
        return Ok(new { message = "User Updated successfully." });
    }

    [HttpDelete("DeleteUser/{id}")]
    public IActionResult Delete(int id)
    {
        if (id <= 0)
            return BadRequest(new { message = "Invalid user ID provided." });

        _service.DeleteUser(id);
        return Ok(new { message = "User deleted successfully." });
    }
}