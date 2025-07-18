using FluentAssertions.Extensions;
using Weav_App.Models.Orders;
using Weav_App.Models.ViewsModel;
using Weav_App.Repositories.Interface;
using Weav_App.Services.Interface;

namespace Weav_App.Services.AdminService.Builder;

public class AdminAccountPageBuilderService : IAdminAccountPageBuilderService
{
    private readonly IAdminAccountPageService _adminService;
    private readonly IOrderService _orderService;
    private readonly IProductServices _productService;

    public AdminAccountPageBuilderService(
        IAdminAccountPageService adminService,
        IOrderService orderService,
        IProductServices productRepo)
    {
        _adminService = adminService;
        _orderService = orderService;
        _productService = productRepo;
    }

    //TODO: the builder here 
    public async Task<AdminAcountUserDataViewModel> BuildAsync(string email)
    {
        //User data
        var user = await _adminService.GetAdminUserData(email);
        
        //Orders data
        var totalOrders = await _orderService.GetAllOrders();
        var pendingOrders = await _orderService.GetAllPendingOrders();
        var shippedOrders = await _orderService.GetAllShippedOrders();
        var todayTotals = await _orderService.GetTodayRevenue();
        
        //Products data
        var totalProducts = await _productService.GetAllProducts();
        
        
    
        return new AdminAcountUserDataViewModel
        {
            UserAccount = user.Data,
            OrderData =
            {
                TotalOrders = totalOrders.Data.Count(),
                PendingOrders = pendingOrders.Data.Count(),
                ShippedOrders = shippedOrders.Data.Count(),
                TodayTotals = todayTotals.Data
            },
            ProductData =
            {
                TotalProducts = totalProducts.Data.Count(),
            }
        };
    }
}