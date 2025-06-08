using Postgrest.Models;
using Supabase;
using Weav_App.DTOs.Entities.Categories;
using Weav_App.DTOs.Entities.User;
using Weav_App.Models;
using Weav_App.Models.ViewsModel;

namespace Weav_App.Helpers;

public class UsefulChecks
{
    private readonly Client _supabase;

    public UsefulChecks(Client supabase)
    {
        _supabase = supabase;
    }
    
    public async Task<T?> RecordExistsAsync<T>(string column, string value) where T : BaseModel, new()
    {
        var result = await _supabase
            .From<T>()
            .Filter(column, Postgrest.Constants.Operator.Equals, value)
            .Get();

        return result.Models.FirstOrDefault();
    }

    public async Task<CategoryDbTable?> GetCategoryByName(string categoryName)
    {
        var result = await _supabase
            .From<CategoryDbTable>()
            .Select("CategoryId")
            .Where(x => x.CategoryName == categoryName).Get();
        
        return result.Models.FirstOrDefault();
    }
    
    public bool ValidateModel(CreateProductViewModel model)
    {
        if (model == null || model.Product == null)
            return false;

        var product = model.Product;

        if (string.IsNullOrWhiteSpace(product.ProductName))
            return false;

        if (string.IsNullOrWhiteSpace(product.Brand))
            return false;

        if (string.IsNullOrWhiteSpace(product.SelectedCategory))
            return false;

        if (product.ProductPrice <= 0.0m)
            return false;

        if (product.Quantity < 0)
            return false;

        if (model.Categories == null || !model.Categories.Contains(product.SelectedCategory))
            return false;

        return true;
    }


}