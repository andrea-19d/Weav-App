using Weav_App.DTOs.Entities.Categories;

namespace Weav_App.DTOs.Entities.Products;

public class ProductDto
{
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; }
    public string? Brand { get; set; }
    public string? ProductCategory { get; set; }
    public string? ProductDescription { get; set; }
    public decimal? ProductPrice { get; set; }
    public int Quantity { get; set; } 
    public DateTime ExpiryDate { get; set; }
    public string Barcode { get; set; }
    public int CategoryId { get; set; } 
}