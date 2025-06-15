using Weav_App.Helpers.Mapper;
using Weav_App.Models;
using Weav_App.Repositories;
using Weav_App.Repositories.Interface;
using Weav_App.Services.AdminService;
using Weav_App.Services.General.InsertStrategies;
using Weav_App.Services.Interface;
using Weav_App.Services.OrderServices;
using Weav_App.Services.ProductServices;
using Weav_App.Services.UserServices;

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
        services.AddScoped<IAdminUserManagementPage, AdminUserManagementPage>();
        
        //User
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStrategy<RegisterUserModel>, AddAdminStrategy>();
        
        //Orders
        services.AddScoped<IOrderService, ManageOrderService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        //Product
        services.AddScoped<IProductServices, ManageProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStrategy<CreateProductModel>, InsertProductStrategy>();
        
        //Category
        services.AddScoped<ICategoryServices, ManageCategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        //AutoMapper Profiles
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddAutoMapper(typeof(MappingProducts));
        services.AddAutoMapper(typeof(MappingOrders));
        services.AddScoped<MapOrdersToDTOsManual>();
        
        //Other
        services.AddScoped<UsefulChecks>();
        services.AddScoped<InsertDispatcher>();
        services.Decorate<IOrderService, LoggingOrderServiceDecorator>();

        
        services.AddHttpContextAccessor();
    }
}