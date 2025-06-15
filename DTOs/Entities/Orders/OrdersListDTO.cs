namespace Weav_App.DTOs.Entities.Orders;

public class OrdersListDTO
{
    public int OrderId { get; set; }
    public string PurchaseOrder { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public short StatusValue { get; set; }
    public OrderStatus Status { get; set; }
    public string StreetName { get; set; }
    public string StreetAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Comments { get; set; }
}