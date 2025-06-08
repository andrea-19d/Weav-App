using Weav_App.DTOs.Entities.Orders;
using Weav_App.Services.General;

namespace Weav_App.Services.Interface;

public interface IOrderService
{
    Task<ServiceResult<List<OrdersListDTO>>> GetAllOrders();
    Task<ServiceResult<List<OrdersListDTO>>> GetAllPendingOrders();
    Task<ServiceResult<List<OrdersListDTO>>> GetAllShippedOrders();
    Task<ServiceResult<decimal>> GetTodayRevenue();
}