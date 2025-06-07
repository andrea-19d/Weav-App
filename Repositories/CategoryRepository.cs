using Supabase;
using Weav_App.DTOs.Entities.Categories;
using Weav_App.Repositories.Interface;

public class CategoryRepository : ICategoryRepository
{
    private readonly Client _supabase;

    public CategoryRepository(Client supabase)
    {
        _supabase = supabase;
    }

    public async Task<List<CategoryDbTable>> GetCategories()
    {
        if (_supabase == null)
        {
            throw new System.ArgumentNullException(nameof(_supabase));
        }
        
        var result = await _supabase.From<CategoryDbTable>().Get();
        return result.Models;
    }
}