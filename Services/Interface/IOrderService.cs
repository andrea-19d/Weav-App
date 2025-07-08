using Weav_App.DTOs;
using Weav_App.DTOs.Entities.Orders;
using Weav_App.Models;
using Weav_App.Services.General;

namespace Weav_App.Services.Interface;

public interface IOrderService
{
    Task<ErrorModel> DeleteOrderByPo(string PoNumberId);
    Task<ServiceResult<List<OrdersListDTO>>> GetAllOrders();
    Task<ServiceResult<List<OrdersListDTO>>> GetAllPendingOrders();
    Task<ServiceResult<List<OrdersListDTO>>> GetAllShippedOrders();
    Task<ServiceResult<decimal>> GetTodayRevenue();
    Task<ServiceResult<List<OrdersListDTO>>> GetAllShippedTodayOrders();
    Task<ServiceResult<List<OrdersListDTO>>> SearchOrderByStatus(OrderStatus status);
    Task<ErrorModel> ChangeOrderStatus(int orderId);
}