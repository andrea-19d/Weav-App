using Weav_App.DTOs.Entities.Products;
using Weav_App.DTOs.Entities.User;
using Weav_App.Models;
using Weav_App.Models.ViewsModel;
using Weav_App.Services.General;

namespace Weav_App.Services.Interface;

public interface IProductServices
{
    Task<ServiceResult<List<ProductDto>>> GetAllProducts();
    Task<ServiceResult<List<ProductDto>>> GetLowStokData();
    
    Task<ServiceResult<List<ProductDto>>> GetInactiveStokData();
    
    Task<ServiceResult<List<ProductDto>>> SearchProductsByCategory(string categoryName);
    Task<ServiceResult<CreateProductViewModel>> CreateProduct(CreateProductModel productModel, string selectedCategory);
}