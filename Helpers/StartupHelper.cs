using Weav_App.Helpers.Mapper;
using Weav_App.Repositories;
using Weav_App.Repositories.Interface;
using Weav_App.Services.AdminService;
using Weav_App.Services.Interface;
using Weav_App.Services.OrderServices;
using Weav_App.Services.ProductServices;

namespace Weav_App.Helpers;

public static class StartupHelper
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        //Authentication
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        
        //Admin
        services.AddScoped<IAdminProductManagementPageService, AdminProductManagementPageService>();
        services.AddScoped<IAdminOrderPageService, AdminOrdersPageService>();
        
        //Orders
        services.AddScoped<IOrderService, ManageOrderService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        //Product
        services.AddScoped<IProductServices, ManageProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        //Category
        services.AddScoped<ICategoryServices, ManageCategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        //AutoMapper Profiles
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddAutoMapper(typeof(MappingProducts));
        services.AddAutoMapper(typeof(MappingOrders));
        
        //Other
        services.AddScoped<UsefulChecks>();
        
        services.AddHttpContextAccessor();
    }
}