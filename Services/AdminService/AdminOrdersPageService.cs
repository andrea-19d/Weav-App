using Weav_App.DTOs;
using Weav_App.DTOs.Entities.Orders;
using Weav_App.Helpers;
using Weav_App.Helpers.HelpersUI;
using Weav_App.Models;
using Weav_App.Models.Orders;
using Weav_App.Models.ViewsModel;
using Weav_App.Services.Interface;

namespace Weav_App.Services.AdminService;

public class AdminOrdersPageService : IAdminOrderPageService
{
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;

    public AdminOrdersPageService(IOrderService orderService, IUserService userService)
    {
        _orderService = orderService;
        _userService = userService;
    }

    public async Task<OrdersPageViewModel> GetOrdersPageViewModel(string? searchQuery, OrderStatus? selectedStatus,
        int page, int pageSize)
    {
        var orderResult = selectedStatus.HasValue
            ? await _orderService.SearchOrderByStatus(selectedStatus.Value)
            : await _orderService.GetAllOrders();

        Console.WriteLine($"Total before filter: {orderResult.Data.Count}");

        var totalOrdersTask = await _orderService.GetAllOrders();
        var pendingOrdersTask = await _orderService.GetAllPendingOrders();
        var shippedTodayOrdersTask = await _orderService.GetAllShippedTodayOrders();
        var todayRevenueTask = await _orderService.GetTodayRevenue();

        if (!orderResult.Success || orderResult.Data == null)
        {
            return new OrdersPageViewModel
            {
                Orders = new List<OrdersListDTO>(),
                Filter = new OrdersFilterModel(),
                Stats = new OrderStats(),
                Pagination = new PaginationData()
            };
        }

        var userIds = orderResult.Data.Select(o => o.UserId).Distinct().ToList();
        var users = await _userService.GetUsersByIds(userIds);
        var userDict = users.ToDictionary(u => u.UserId, u => u);

        var filtered = SearchHelper.FilterByQuery(orderResult.Data, searchQuery, p =>
        [
            p.PurchaseOrder,
            p.UserId.ToString()
        ]);

        Console.WriteLine($"Filtered count: {filtered.Count}");

        var paginated = PaginationHelper.Paginate(filtered, page, pageSize);

        foreach (var order in paginated.Items)
        {
            if (userDict.TryGetValue(order.UserId, out var user))
            {
                order.UserName = user.Username;
                order.Email = user.Email;
            }
        }

        Console.WriteLine($"Page {page}/{paginated.TotalPages}, Items on page: {paginated.Items.Count}");

        return new OrdersPageViewModel
        {
            Orders = paginated.Items,
            Filter = new OrdersFilterModel
            {
                SearchQuery = searchQuery,
                SelectedStatus = selectedStatus,
                Status = EnumHelper.GetEnumMemberValues<OrderStatus>(),
                DateFrom = DateOnly.FromDateTime(DateTime.Now),
                Priority = EnumHelper.GetEnumMemberValues<Priority>(),
            },
            Stats = new OrderStats
            {
                TotalOrders = totalOrdersTask.Data.Count,
                PendingOrders = pendingOrdersTask.Data.Count,
                ShippedOrders = shippedTodayOrdersTask.Data.Count,
                TodayTotals = todayRevenueTask.Data
            },
            Pagination = new PaginationData
            {
                CurrentPage = paginated.CurrentPage,
                TotalPages = paginated.TotalPages,
            }
        };
    }
}