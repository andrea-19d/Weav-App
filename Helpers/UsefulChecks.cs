using Supabase.Postgrest;
using Supabase.Postgrest.Models;
using Weav_App.DTOs.Entities.Categories;
using Weav_App.Models;
using Client = Supabase.Client;

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
            .Filter(column, Constants.Operator.Equals, value)
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
    
    public bool ValidateModel(CreateProductModel model)
    {

        if (string.IsNullOrWhiteSpace(model.ProductName))
            return false;

        if (string.IsNullOrWhiteSpace(model.Brand))
            return false;

        if (string.IsNullOrWhiteSpace(model.SelectedCategory))
            return false;

        if (model.ProductPrice <= 0.0m)
            return false;

        if (model.Quantity < 0)
            return false;

        if (model.Categories == null || !model.Categories.Contains(model.SelectedCategory))
            return false;

        return true;
    }


}