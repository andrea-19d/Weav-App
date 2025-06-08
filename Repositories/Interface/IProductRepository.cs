using Weav_App.DTOs.Entities.Categories;
using Weav_App.DTOs.Entities.Products;

namespace Weav_App.Repositories.Interface;

public interface IProductRepository
{
    Task<List<ProductDbTable>> GetAllAsync();
    Task<List<ProductDbTable>> GetLowStokData();
    
    Task<List<ProductDbTable>> GetInactiveStokData();
    
    Task<List<ProductDbTable>> GetProductByCategory(string category);
    Task<(bool succes, string? error)> CreateNewProduct(ProductDto productDto, string selectedCategory);
}