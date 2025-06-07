using Weav_App.Models;
using Weav_App.Services.General;

namespace Weav_App.Services.Interface;

public interface ICategoryServices
{
    Task<ServiceResult<List<CategoryModel>>> GetAllCategories();
}
