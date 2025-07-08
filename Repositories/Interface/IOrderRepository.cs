using Weav_App.DTOs.Entities.Orders;
using Weav_App.Models;

namespace Weav_App.Repositories.Interface;

public interface IOrderRepository
{
    Task<ErrorModel> DeleteOrder(string id);
    Task<List<OrdersDbTable>> GetAllOrdersAsync();
    Task<List<OrdersDbTable>> GetAllPendingAsync();
    Task<List<OrdersDbTable>> GetAllShippedAsync();
    Task<List<OrdersDbTable>> GetAllShippedTodayAsync();
    Task<List<OrdersDbTable>> TodayRevenueAsync();
    Task<List<OrdersDbTable>> GetOrderByStatus(OrderStatus status);
    Task<ErrorModel> ConfirmOrderById(int id);
}