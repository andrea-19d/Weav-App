using Weav_App.Models;
using Weav_App.Services.General;

namespace Weav_App.Services.Interface;

public interface IAdminUserManagementPage
{
    Task<ServiceResult<RegisterUserModel>> AddAdmin(RegisterUserModel model);
}