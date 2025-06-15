using Weav_App.DTOs;
using Weav_App.DTOs.Entities.User;

namespace Weav_App.Repositories.Interface;

public interface IAuthRepository
{
    Task<(bool success, string? error)> RegisterUserAsync(RegisterUserDto registerUserDto, UserLevel level);
    
    Task<(bool succes, UserLevel level)> LoginUserAsync(LoginUserDto loginUserDto, string password);

}