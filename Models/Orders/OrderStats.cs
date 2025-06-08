namespace Weav_App.Models.Orders;

public class OrderStats
{
    public int TotalOrders { get; set; }
    public int PendingOrders { get; set; }
    public int ShippedOrders { get; set; }
    public decimal TodayTotals { get; set; }
}