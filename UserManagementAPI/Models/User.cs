using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserManagementAPI.Models;

public class User
{
    public int UserId { get; set; }
    [EmailAddress]
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public required DateTime DOB { get; set; }
    [JsonIgnore]
    public string CreatedBy { get; set; }
    [JsonIgnore]
    public DateTime CreatedDate { get; set; }
    [JsonIgnore]
    public string? ModifiedBy { get; set; }
    [JsonIgnore]
    public DateTime? ModifiedDate { get; set; }

    public User()
    {
        CreatedBy = "admin@gmail.com";
        CreatedDate = DateTime.UtcNow;
    }
}