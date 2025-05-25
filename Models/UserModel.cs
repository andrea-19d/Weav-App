    using Weav_App.DTOs;

    namespace Weav_App.Models
    {
        public class UserModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserIP { get; set; }
            public DateTime RegisterDate { get; set; }
            public DateTime LastLogin { get; set; }
            public byte[] UserPhoto { get; set; }
            public UserLevel Level {get; set;} 
            public UserModel(string userName, string email, string userIP, DateTime registerDate, DateTime lastLogin, string password, byte[] userPhoto, UserLevel level)
            {   
                UserName = userName;
                Email = email;
                UserIP = userIP;
                RegisterDate = registerDate;
                LastLogin = lastLogin;
                Password = password;
                UserPhoto = userPhoto;
                Level = level;
            }
        }

        public class LogInUser
        {
            public string UserName { get; set; } = "";
            public string Password { get; set; } = "";
            public bool RememberMe { get; set; } = false;
            public UserLevel  Level { get; set; } 
        }

        public class RegisterUser
        {
            public string UserName { get; set; } = "";
            public string Password { get; set; } = "";
            public string Email { get; set; } = "";
            public string? UserIP { get; set; }
            public byte[]? UserPhoto { get; set; }
            public UserLevel Level  { get; set; } 
            public string RePassword {get; set;} = "";
            public bool AgreeToTerms { get; set; } =  false;
        }
    }