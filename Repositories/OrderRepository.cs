using Supabase;
using Weav_App.DTOs;
using Weav_App.DTOs.Entities.Orders;
using Weav_App.Repositories.Interface;

namespace Weav_App.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Client _supabase;


    public OrderRepository(Client supabase)
    {
        _supabase = supabase;
    }
    
    public async Task<List<OrdersDbTable>> GetAllOrdersAsync()
    {
        var orders = await _supabase.From<OrdersDbTable>().Get();
        return orders.Models;
    }

    public async Task<List<OrdersDbTable>> GetAllPendingAsync()
    {
        var orders = await _supabase.From<OrdersDbTable>()
            .Where(x => x.Status == OrderStatus.Pending).Get();
        
        return orders.Models;
    }

    public async Task<List<OrdersDbTable>> GetAllShippedAsync()
    {
        var orders = await _supabase.From<OrdersDbTable>()
            .Where(x => x.Status == OrderStatus.Shipped).Get();
        
        return orders.Models;
    }

    public async Task<List<OrdersDbTable>> TodayRevenueAsync()
    {
        var todayRevenue = await _supabase.From<OrdersDbTable>()
            .Select("TotalAmount").Get(); 
        
        return todayRevenue.Models;
    }
    
    // public async Task<List<OrdersDbTable>> CreateNewOrderAsync()
    // {
    //     
    // }
}