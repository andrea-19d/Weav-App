using Weav_App.DTOs.Entities.User;

namespace Weav_App.Services.Interface;

public interface IUserService
{
    Task<bool> RegisterUserAsync(RegisterUserDTO registerUserDto);
    
    Task<bool> LoginUserAsync(LoginUserDTO loginUserDto, string password);
}
