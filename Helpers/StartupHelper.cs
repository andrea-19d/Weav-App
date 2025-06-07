using Weav_App.Repositories;
using Weav_App.Repositories.Interface;
using Weav_App.Services.Interface;
using Weav_App.Services.ProductServices;

namespace Weav_App.Helpers;

public static class StartupHelper
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IProductServices, ManageProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryServices, ManageCategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddAutoMapper(typeof(Program));
        services.AddHttpContextAccessor();
    }
}