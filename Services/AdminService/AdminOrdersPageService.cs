using Weav_App.DTOs;
using Weav_App.Helpers.HelpersUI;
using Weav_App.Models;
using Weav_App.Models.Orders;
using Weav_App.Models.ViewsModel;
using Weav_App.Services.Interface;

namespace Weav_App.Services.AdminService;

public class AdminOrdersPageService : IAdminOrderPageService
{
    private readonly IOrderService _orderService;
    
    public AdminOrdersPageService(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    public async Task<OrdersPageViewModel> GetOrdersPageViewModel(string? searchQuery, string? selectedStatus, int page,
        int pageSize)
    {
        
        // var orderResult = await _orderService.GetAllOrders();
        var orderResult = !string.IsNullOrWhiteSpace(selectedStatus)
            ? await _orderService.GetAllOrders()
            : null;
        
        if(!orderResult.Success || orderResult.Data == null )
            return new OrdersPageViewModel(); ;
        
        var pendigOrdersResult = await _orderService.GetAllPendingOrders();
        var shippedOrdersResult = await _orderService.GetAllShippedOrders();
        var totalOrderrevenueResult = await _orderService.GetTodayRevenue();
        
        var filtered = SearchHelper.FilterByQuery(orderResult.Data, searchQuery, p =>
            [
                p.PurchaseOrder,
                p.UserId.ToString()
            ]);
        
        var paginated = PaginationHelper.Paginate(filtered, page, pageSize);

        return new OrdersPageViewModel
        {
            Orders = paginated.Items,
            Filter = new OrdersFilterModel
            {
                SearchQuery = searchQuery,
                SelectedStatus = selectedStatus,
                Status = Enum.GetNames(typeof(OrderStatus)).ToList().ToString(),
                DateFrom = DateOnly.FromDateTime(DateTime.Now),
                Priority = Enum.GetNames(typeof(Priority)).ToList().ToString()
            },
            Stats = new OrderStats
            {
                TotalOrders = pendigOrdersResult.Data.Count,
                PendingOrders = shippedOrdersResult.Data.Count,
                ShippedOrders = shippedOrdersResult.Data.Count,
                TodayTotals = totalOrderrevenueResult.Data
            },
            Pagination = new PaginationData
            {
                CurrentPage = paginated.CurrentPage,
                TotalPages = paginated.TotalPages,
            }
        };
    }
}