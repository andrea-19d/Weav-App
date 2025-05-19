namespace Weav_App.Models;

public class UserModel
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public List<string> Roles { get; set; }
    public bool IsActive { get; set; }
    public string? Token { get; set; }
    public string Password { get; set; }

    public UserModel(string userName, string name, List<string> roles, bool isActive, string? token, string password)
    {   
        UserName = userName;
        Name = name;
        Roles = roles;
        IsActive = isActive;
        Token = token;
        Password = password;
    }

    public class LogInUser
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class RegisterUser
    {
        public string Name { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public List<string> Roles { get; set; }
    }
}