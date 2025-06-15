using Weav_App.DTOs;
using Weav_App.DTOs.Entities.User;

namespace Weav_App.Services.Interface;

public interface IUserService
{
    Task<List<UserDbTable>> GetUsersByIds(List<int> userIds);
}
