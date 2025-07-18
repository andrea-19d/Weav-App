using Weav_App.Models;
using Weav_App.Services.General;

namespace Weav_App.Services.Interface;

public interface IAdminAccountPageService
{
    Task<ServiceResult<UserAccountData>> GetAdminUserData(string email);
    // Task<ErrorModel> UpdateUserData(UserModel user);
}