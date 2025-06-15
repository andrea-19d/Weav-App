using Weav_App.DTOs;
using Weav_App.DTOs.Entities.User;

namespace Weav_App.Services.Interface;

public interface IAuthService
{
    Task<(bool succes, UserLevel level )> LoginUser(LoginUserDto user, string password);
    Task<(bool succes, string? error)> RegisterUser(RegisterUserDto user);
    Task<(bool succes, string? error)> RegisterAdmin(RegisterUserDto user);
    
}