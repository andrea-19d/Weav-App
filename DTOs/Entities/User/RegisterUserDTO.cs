namespace Weav_App.DTOs.Entities.User;

public class RegisterUserDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserIP { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime LastLogin { get; set; }
    public byte[] UserPhoto { get; set; }
    public UserLevel Level { get; set; }
}