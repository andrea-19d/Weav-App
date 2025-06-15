using Weav_App.Models;
using Weav_App.Models.ViewsModel;
using Weav_App.Services.General;

namespace Weav_App.Services.Interface;

public interface IAdminProductManagementPageService
{
    Task<ProductManagementViewModel> GetProductManagementViewModel(string? searchQuery, string? selectedCategory, int page, int pageSize);

    Task<ServiceResult<CreateProductModel>> GetCreateProductViewModel(CreateProductModel model);
}
