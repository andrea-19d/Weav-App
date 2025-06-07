using Weav_App.DTOs.Entities.Categories;

namespace Weav_App.DTOs.Entities.Products;

public class ProductDto
{
    public string? ProductImage { get; set; }
    public string? ProductName { get; set; }
    public string? Brand { get; set; }
    public string? ProductCategory { get; set; }
    public string? ProductPrice { get; set; }
    public int Quantity { get; set; } 
}