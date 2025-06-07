namespace Weav_App.DTOs.Entities.User;

public class LoginUserDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public UserLevel Level { get; set; }
}