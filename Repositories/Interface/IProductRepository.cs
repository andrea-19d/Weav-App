using Weav_App.DTOs.Entities.Categories;
using Weav_App.DTOs.Entities.Products;
using Weav_App.Models;

namespace Weav_App.Repositories.Interface;

public interface IProductRepository
{
    Task<ErrorModel> DeleteProduct(int productId);
    Task<ProductDbTable> GetProductByIdAsync(int id);
    Task<ErrorModel> UpdateProductAsync(ProductDbTable product);
    Task<List<ProductDbTable>> GetAllAsync();
    Task<List<ProductDbTable>> GetLowStokData();
    
    Task<List<ProductDbTable>> GetInactiveStokData();
    
    Task<List<ProductDbTable>> GetProductByCategory(string category);
    Task<(bool succes, string? error)> CreateNewProduct(ProductDto productDto, string selectedCategory);
}