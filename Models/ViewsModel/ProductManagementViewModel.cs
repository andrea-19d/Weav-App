using Weav_App.DTOs.Entities.Products;

namespace Weav_App.Models.ViewsModel;

public class ProductManagementViewModel : GeneralPageViewModel
{
    public List<ProductDto>? Products { get; set; } = new();
    public ProductFilterModel Filter { get; set; } = new();
    public ProductStats Stats { get; set; } = new();
}