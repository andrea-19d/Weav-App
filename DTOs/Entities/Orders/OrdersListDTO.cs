namespace Weav_App.DTOs.Entities.Orders;

public class OrdersListDTO
{
    public string PurchaseOrder { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}