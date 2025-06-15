namespace Weav_App.DTOs.Entities.User;

public class RegisterUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserIp { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime LastLogin { get; set; }
    public string UserPhotoUrl { get; set; }
    
    public UserLevel Level { get; set; }
}