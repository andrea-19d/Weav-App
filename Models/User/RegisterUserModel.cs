using Weav_App.DTOs;

namespace Weav_App.Models;

public class RegisterUserModel
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string Email { get; set; } = "";
    public string? UserIp { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string? UserPhotoUrl { get; set; }
    public UserLevel Level  { get; set; } 
    public string RePassword {get; set;} = "";
    public bool AgreeToTerms { get; set; } =  false;
}