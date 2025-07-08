using AutoMapper;
using Weav_App.DTOs;
using Weav_App.DTOs.Entities.Orders;
using Weav_App.Models;
using Weav_App.Repositories.Interface;
using Weav_App.Services.General;
using Weav_App.Services.Interface;

namespace Weav_App.Services.OrderServices;

public class ManageOrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly IMapper _mapper;

    public ManageOrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ErrorModel> DeleteOrderByPo(string PoNumberId)
    {
        var deletedOrder = await _orderRepository.DeleteOrder(PoNumberId);
        Console.WriteLine($"{deletedOrder.Message}");

        return new ErrorModel()
        {
            Message = deletedOrder.Message,
            Status = deletedOrder.Status
        };
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> GetAllOrders()
    {
        var ordersDb = await _orderRepository.GetAllOrdersAsync();
        var dtoOrders = _mapper.Map<List<OrdersListDTO>>(ordersDb).ToList();
        return new ServiceResult<List<OrdersListDTO>>
        {
            Success = true,
            Data = dtoOrders,
            ErrorMessage = "Task ended successfully"
        };
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> GetAllPendingOrders()
    {
        var ordersDb = await _orderRepository.GetAllPendingAsync();
        var dtoOrders = _mapper.Map<List<OrdersListDTO>>(ordersDb).ToList();

        return new ServiceResult<List<OrdersListDTO>>
        {
            Success = true,
            Data = dtoOrders,
            ErrorMessage = "Task ended successfully"
        };
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> GetAllShippedOrders()
    {
        var ordersDb = await _orderRepository.GetAllShippedAsync();
        var dtoOrders = _mapper.Map<List<OrdersListDTO>>(ordersDb).ToList();

        return new ServiceResult<List<OrdersListDTO>>
        {
            Success = true,
            Data = dtoOrders,
            ErrorMessage = "Task ended successfully"
        };
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> GetAllShippedTodayOrders()
    {
        var ordersDb = await _orderRepository.GetAllShippedTodayAsync();
        var dtoOrders = _mapper.Map<List<OrdersListDTO>>(ordersDb).ToList();

        return new ServiceResult<List<OrdersListDTO>>
        {
            Success = true,
            Data = dtoOrders,
            ErrorMessage = "Task ended successfully"
        };
    }

    public async Task<ServiceResult<decimal>> GetTodayRevenue()
    {
        var todayRevenue = await _orderRepository.TodayRevenueAsync();
        var todayRevenueDecimal = 0.0m;

        foreach (var revenue in todayRevenue)
        {
            todayRevenueDecimal += revenue.TotalAmount;
        }

        return new ServiceResult<decimal>
        {
            Success = true,
            Data = todayRevenueDecimal,
            ErrorMessage = "Task ended successfully"
        };
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> SearchOrderByStatus(OrderStatus status)
    {
        var dbOrders = await _orderRepository.GetOrderByStatus(status);
        var dtoOrders = _mapper.Map<List<OrdersListDTO>>(dbOrders);
        
        return new ServiceResult<List<OrdersListDTO>>
        {
            Success = true,
            Data = dtoOrders,
            ErrorMessage = "Task ended successfully"
        };
    }

    public async Task<ErrorModel> ChangeOrderStatus(int purchaseOrder)
    {
        var changedOrder =await  _orderRepository.ConfirmOrderById(purchaseOrder);

        if (!changedOrder.Status)
        {
            return new ErrorModel
            {
                Status = false,
                Message = "Order could not be confirmed"
            };
        }
        
        return new ErrorModel
        {
            Status = true,
            Message = "Order confirmed successfully"
        };
    }
}