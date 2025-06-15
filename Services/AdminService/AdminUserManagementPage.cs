using Microsoft.AspNetCore.Mvc;
using Weav_App.Models;
using Weav_App.Services.General;
using Weav_App.Services.General.InsertStrategies;
using Weav_App.Services.Interface;

namespace Weav_App.Services.AdminService;

public class AdminUserManagementPage : IAdminUserManagementPage
{
    private readonly IStrategy<RegisterUserModel> _insertProductStrategy;

    public AdminUserManagementPage(IStrategy<RegisterUserModel> insertProductStrategy)
    {
        _insertProductStrategy = insertProductStrategy;
    }

    public async Task<ServiceResult<RegisterUserModel>> AddAdmin(RegisterUserModel model)
    {
        var newAdmin = await _insertProductStrategy.CreateItem(model);

        if (!newAdmin.Success && newAdmin.Data == null)
        {
            return  new ServiceResult<RegisterUserModel>
            {
                Success = false,
                Data = null,
                ErrorMessage = newAdmin.ErrorMessage
            };
        }
        
        return new ServiceResult<RegisterUserModel>
        {
            Success = true,
            Data = newAdmin.Data,
            ErrorMessage = "Registration Successful",
        };
    }
    
}