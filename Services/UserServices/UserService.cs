using Weav_App.DTOs.Entities.User;
using Weav_App.Repositories.Interface;
using Weav_App.Services.General;
using Weav_App.Services.Interface;

namespace Weav_App.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<List<UserDbTable>> GetUsersByIds(List<int> userIds)
    {
        return await _userRepository.GetUsersByIds(userIds);
    }
}