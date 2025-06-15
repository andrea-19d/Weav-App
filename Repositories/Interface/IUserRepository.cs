using Weav_App.DTOs.Entities.User;

namespace Weav_App.Repositories.Interface;

public interface IUserRepository
{
    Task<List<UserDbTable>> GetUsersByIds(List<int> userIds);
}