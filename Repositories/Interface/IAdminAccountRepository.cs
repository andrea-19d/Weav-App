using Weav_App.DTOs.Entities.User;
using Weav_App.Models;
using Weav_App.Services.General;

namespace Weav_App.Repositories.Interface;

public interface IAdminAccountRepository
{
    Task<ServiceResult<UserDbTable>> GetAdminData(string email);
    Task<ErrorModel> UpdateAdminData(UserDbTable model);
}