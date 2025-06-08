using Postgrest.Attributes;
using Postgrest.Models;
using Weav_App.DTOs.Entities.Products;

namespace Weav_App.DTOs.Entities.Orders;

[Table("OrderItems")]
public class OrderItemDbTable : BaseModel
{
    [Column("OrderItemId")]
    public int OrderItemId { get; set; }

    [Column("OrderId")]
    public int OrderId { get; set; }

    [Column("ProductId")]
    public int ProductId { get; set; }

    [Column("Quantity")]
    public int Quantity { get; set; }

    [Column("PricePerUnit")]
    public decimal PricePerUnit { get; set; }

    public OrdersDbTable Order { get; set; }
    public ProductDbTable Product { get; set; }
}
