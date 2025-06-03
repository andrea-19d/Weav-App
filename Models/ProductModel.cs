using Weav_App.DTOs;

namespace Weav_App.Models;

public class ProductModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Brand { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
    
    public UnitsEnums Units { get; set; }
    
    public string? ProductDescription { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public ProductModel(){}

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
    
}