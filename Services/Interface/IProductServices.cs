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
    Task<ErrorModel> DeleteProduct(int id);
    Task<ServiceResult<ProductDto>> GetProductByIdAsync(int id);
    Task<ErrorModel> UpdateProduct(ProductDto product);

}