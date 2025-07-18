using NUnit.Framework;
using Supabase.Postgrest;
using Weav_App.DTOs.Entities.Orders;
using Weav_App.Models;
using Weav_App.Repositories.Interface;
using Client = Supabase.Client;

namespace Weav_App.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Client _supabase;
    
    public OrderRepository(Client supabase)
    {
        _supabase = supabase;
    }

    public async Task<ErrorModel> DeleteOrder(string id)
    {
        try
        {
            await _supabase.From<OrdersDbTable>().Where(x => x.PurchaseOrder == id).Delete();

            return new ErrorModel()
            {
                Message = "Order deleted",
                Status = true
            };
        }
        catch (Exception e)
        {
            return new ErrorModel()
            {
                Message = e.Message,
                Status = false
            };
        }
    }
    
    //TODO: crete order function
    
    public async Task<List<OrdersDbTable>> GetAllOrdersAsync()
    {
        var orders = await _supabase.From<OrdersDbTable>().Get();
        Console.WriteLine($"Fetched all orders: {orders.Models.Count}");
        return orders.Models;
    }

    public async Task<List<OrdersDbTable>> GetAllPendingAsync()
    {
        var orders = await _supabase.From<OrdersDbTable>()
            .Where(x => x.Status == OrderStatus.Pending)
            .Order(x => x.OrderDate, Constants.Ordering.Descending)
            .Get();
        
        return orders.Models;
    }

    public async Task<List<OrdersDbTable>> GetAllShippedAsync()
    {
        var orders = await _supabase.From<OrdersDbTable>()
            .Where(x => x.Status == OrderStatus.Shipped).Get();
        
        return orders.Models;
    }

    public async Task<List<OrdersDbTable>> GetAllShippedTodayAsync()
    {
        var orders = await _supabase.From<OrdersDbTable>()
            .Where(x => x.OrderDate >= DateTime.Today && x.Status == OrderStatus.Shipped)
            .Get();
        
        return orders.Models;
    }

    public async Task<List<OrdersDbTable>> TodayRevenueAsync()
    {
        var today = DateTime.UtcNow.Date;

        var todayRevenue = await _supabase
            .From<OrdersDbTable>()
            .Where(x => x.OrderDate >= today)
            .Get();
        
        return todayRevenue.Models;
    }

    public async Task<List<OrdersDbTable>> GetOrderByStatus(OrderStatus status)
    {
        var statusInt = (int)status;
        var orders = await _supabase
            .From<OrdersDbTable>()
            .Filter("Status", Constants.Operator.Equals, statusInt)
            .Get();
        Console.WriteLine($"Fetched from Supabase: {orders.Models.Count} orders with Status = {statusInt}");
        return orders.Models;
    }
    
    public async Task<ErrorModel> ConfirmOrderById(int id)
    {
        var updateResult = await _supabase
            .From<OrdersDbTable>()
            .Where(x => x.OrderId == id)
            .Set(x => x.StatusValue, (short)OrderStatus.Confirmed) 
            .Update(new QueryOptions { Returning = QueryOptions.ReturnType.Representation });

        if (updateResult.Models.Any())
        {
            return new ErrorModel
            {
                Status = true,
                Message = "Order confirmed successfully"
            };
        }
        return new ErrorModel
        {
            Status = false,
            Message = "Order not found or update failed"
        };
    }



}