﻿namespace Weav_App.DTOs.Entities.User;

public class LoginUserDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}