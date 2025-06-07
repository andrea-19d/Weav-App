using Weav_App.DTOs.Entities.Products;

namespace Weav_App.Models;

public class ProductFilterModel
{
    public string? SearchQuery { get; set; }
    
    public List<string>? Category { get; set; }
    public string? SelectedCategory { get; set; } 
    public string? Status { get; set; }
}