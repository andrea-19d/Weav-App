using Weav_App.Models.ViewsModel;

namespace Weav_App.Services.AdminService.Builder;

public interface IAdminAccountPageBuilderService
{
    Task<AdminAcountUserDataViewModel> BuildAsync(string email);
}