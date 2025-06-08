using Weav_App.DTOs.Entities.Orders;

namespace Weav_App.Repositories.Interface;

public interface IOrderRepository
{
    Task<List<OrdersDbTable>> GetAllOrdersAsync();
    Task<List<OrdersDbTable>> GetAllPendingAsync();
    Task<List<OrdersDbTable>> GetAllShippedAsync();
    Task<List<OrdersDbTable>> TodayRevenueAsync();
}