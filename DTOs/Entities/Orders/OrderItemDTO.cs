using Weav_App.DTOs.Entities.Products;

namespace Weav_App.DTOs.Entities.Orders;

public class OrderItemDTO
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }

}