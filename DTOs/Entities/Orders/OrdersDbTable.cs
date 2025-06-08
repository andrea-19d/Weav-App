using Postgrest.Attributes;
using Postgrest.Models;
using Weav_App.DTOs.Entities.User;

namespace Weav_App.DTOs.Entities.Orders;

[Table("Orders")]
public class OrdersDbTable : BaseModel
{
    [Column("OrderId")]
    public int OrderId { get; set; }
    
    [Column("PurchaseOrder")]
    public string PurchaseOrder { get; set; } = String.Empty;
    
    [Column("UserId")]
    public int UserId { get; set; }
    
    [Column("CreatedAt")]
    public DateTime OrderDate { get; set; }
    
    [Column("TotalAmount")]
    public decimal TotalAmount { get; set; }
    
    [Column("Status")]
    public OrderStatus Status { get; set; }
    
    public List<OrderItemDbTable> Items { get; set; } = new();
    public UserDbTable User { get; set; }
    
}