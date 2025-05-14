using System.ComponentModel.DataAnnotations;

namespace Weav_App.Models;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
    [Required]
    public string Password { get; set; }
}