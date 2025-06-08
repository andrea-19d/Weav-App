using Weav_App.Helpers;
using Weav_App.Helpers.HelpersUI;
using Weav_App.Models;
using Weav_App.Models.ViewsModel;
using Weav_App.Services.Interface;

namespace Weav_App.Services.AdminService;

public class AdminProductManagementPageService : IAdminProductManagementPageService
{
    private readonly IProductServices _productServices;
    private readonly ICategoryServices _categoryServices;
    private readonly IWebHostEnvironment _environment;

    
    public AdminProductManagementPageService(IProductServices productServices, 
        ICategoryServices categoryServices, UsefulChecks checks ,IWebHostEnvironment environment)
    {
        _productServices = productServices;
        _categoryServices = categoryServices;
        _environment = environment;
    }

    public async Task<ProductManagementViewModel> GetProductManagementViewModel(string? searchQuery, string? selectedCategory, int page, int pageSize)
    {
        var productResult = !string.IsNullOrEmpty(selectedCategory)
            ? await _productServices.SearchProductsByCategory(selectedCategory)
            : await _productServices.GetAllProducts();

        if (!productResult.Success || productResult.Data == null)
            return new ProductManagementViewModel();

        var categoryResult = await _categoryServices.GetAllCategories();
        var lowStock = await _productServices.GetLowStokData();
        var inactiveStock = await _productServices.GetInactiveStokData();

        var filtered = SearchHelper.FilterByQuery(productResult.Data, searchQuery, p =>
        [
            p.ProductName,
            p.Brand,
            p.ProductCategory
        ]);

        var paginated = PaginationHelper.Paginate(filtered, page, pageSize);

        return new ProductManagementViewModel
        {
            Products = paginated.Items,
            Filter = new ProductFilterModel
            {
                SearchQuery = searchQuery,
                SelectedCategory = selectedCategory,
                Category = categoryResult.Data?.Select(c => c.CategoryName).ToList()
            },
            Stats = new ProductStats
            {
                TotalProducts = productResult.Data.Count,
                TotalCategory = categoryResult.Data.Count,
                LowStock = lowStock.Data.Count,
                Inactive = inactiveStock.Data.Count
            },
            Pagination = new PaginationData
            {
                CurrentPage = paginated.CurrentPage,
                TotalPages = paginated.TotalPages
            }
        };
    }

    public async Task<(bool Success, string? ErrorMessage, CreateProductModel model)> GetCreateProductViewModel(CreateProductModel model,
        string selectedCategory)
    {
        var categoryResult = await _categoryServices.GetAllCategories();
        model.Categories = categoryResult.Data?.Select(c => c.CategoryName).ToList();
        
        if (model.ProductImage != null)
        {
            var imagePath = await ImageHelper.SaveProductImageAsync(model.ProductImage, _environment.WebRootPath);
            model.ImageUrl = imagePath;
        }

        if (model == null)
        {
            return (false, "Please correct the errors in the form.", model);
        }
        
        var result = await _productServices.CreateProduct(model, selectedCategory);

        if (!result.Success)
        {
            return (false, result.ErrorMessage ?? "There was a problem creating the product.", model);
        }

        return (true, result.ErrorMessage, model);
    }
}
