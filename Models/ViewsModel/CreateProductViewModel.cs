namespace Weav_App.Models.ViewsModel;

public class CreateProductViewModel
{
    public CreateProductModel Product { get; set; } = new();
    public List<string> Categories { get; set; } = new();
    
    public string? SelectedCategory { get; set; }
}