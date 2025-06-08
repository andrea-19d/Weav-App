namespace Weav_App.Models;

public class CreateProductModel
{
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile? ProductImage { get; set; } 
    public int CategoryId { get; set; }
    public string? Brand { get; set; }
    public decimal? ProductPrice { get; set; }
    public int Quantity { get; set; } 
    public DateTime ExpiryDate { get; set; }
    public string Barcode { get; set; }
    public string? ProductDescription { get; set; }
    public List<string> Categories { get; set; }
    public string? SelectedCategory { get; set; }
}
