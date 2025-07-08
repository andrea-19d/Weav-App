using Weav_App.DTOs;
using Weav_App.DTOs.Entities.Orders;
using Weav_App.Models;
using Weav_App.Services.General;
using Weav_App.Services.Interface;

public class LoggingOrderServiceDecorator : IOrderService
{
    private readonly IOrderService _inner;
    private readonly ILogger<LoggingOrderServiceDecorator> _logger;

    
    
    public LoggingOrderServiceDecorator(IOrderService inner, ILogger<LoggingOrderServiceDecorator> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public Task<ErrorModel> DeleteOrderByPo(string PoNumberId)
    {
        _logger.LogInformation("Delete orders by PO numbers...");
        var result = _inner.DeleteOrderByPo(PoNumberId);
        _logger.LogInformation($"Deleted order {PoNumberId}");
        return result;
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> GetAllOrders()
    {
        _logger.LogInformation("Fetching all orders...");
        var result = await _inner.GetAllOrders();
        _logger.LogInformation("Fetched {Count} orders.", result.Data?.Count ?? 0);
        return result;
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> GetAllPendingOrders()
    {
        _logger.LogInformation("Fetching pending orders...");
        var result = await _inner.GetAllPendingOrders();
        _logger.LogInformation("Fetched {Count} pending orders.", result.Data?.Count ?? 0);
        return result;
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> GetAllShippedOrders()
    {
        _logger.LogInformation("Fetching shipped orders...");
        var result = await _inner.GetAllShippedOrders();
        _logger.LogInformation("Fetched {Count} shipped orders.", result.Data?.Count ?? 0);
        return result;
    }

    public async Task<ServiceResult<decimal>> GetTodayRevenue()
    {
        _logger.LogInformation("Calculating today's revenue...");
        var result = await _inner.GetTodayRevenue();
        _logger.LogInformation("Today's revenue: ${Revenue}", result.Data);
        return result;
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> GetAllShippedTodayOrders()
    {
        _logger.LogInformation("Fetching shipped orders for today...");
        var result = await _inner.GetAllShippedTodayOrders();
        _logger.LogInformation("Fetched {Count} shipped today orders.", result.Data?.Count ?? 0);
        return result;
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> SearchOrderByStatus(OrderStatus status)
    {
        _logger.LogInformation("Searching orders by status: {Status}", status);
        var result = await _inner.SearchOrderByStatus(status);
        _logger.LogInformation("Found {Count} orders with status {Status}.", result.Data?.Count ?? 0, status);
        return result;
    }

    public async Task<ErrorModel> ChangeOrderStatus(int orderId)
    {
        _logger.LogInformation("Attempting to change status for Order ID: {OrderId}", orderId);
        var result = await _inner.ChangeOrderStatus(orderId);

        if (result.Status)
            _logger.LogInformation("Order {OrderId} status updated successfully.", orderId);
        else
            _logger.LogWarning("Failed to update Order {OrderId} status: {Message}", orderId, result.Message);

        return result;
    }
}
