using Weav_App.DTOs.Entities.Categories;

namespace Weav_App.Repositories.Interface;

public interface ICategoryRepository
{
    Task<List<CategoryDbTable>> GetCategories();
}