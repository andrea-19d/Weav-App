using Weav_App.DTOs;

namespace Weav_App.Models;

public class UserAccountData
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserIp { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime LastLogin { get; set; }
    public byte[] UserPhoto { get; set; }
    public UserLevel Level {get; set;} 
}