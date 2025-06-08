using Weav_App.Models;
using Weav_App.Models.ViewsModel;

namespace Weav_App.Services.Interface;

public interface IAdminProductManagementPageService
{
    Task<ProductManagementViewModel> GetProductManagementViewModel(string? searchQuery, string? selectedCategory, int page, int pageSize);

    Task<(bool Success, string? ErrorMessage, CreateProductModel model)> GetCreateProductViewModel(
        CreateProductModel model, string selectedCategory);
}
