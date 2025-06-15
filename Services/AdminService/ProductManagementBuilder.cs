using Weav_App.Helpers;
using Weav_App.Helpers.HelpersUI;
using Weav_App.Models;
using Weav_App.Models.ViewsModel;
using Weav_App.Services.General;
using Weav_App.Services.General.InsertStrategies;
using Weav_App.Services.Interface;

namespace Weav_App.Services.AdminService;

public class AdminProductManagementPageService : IAdminProductManagementPageService
{
    private readonly IProductServices _productServices;
    private readonly ICategoryServices _categoryServices;
    private readonly IWebHostEnvironment _environment;
    private readonly IStrategy<CreateProductModel> _insertProductStrategy;
    
    public AdminProductManagementPageService(IProductServices productServices, 
        ICategoryServices categoryServices, UsefulChecks checks ,IWebHostEnvironment environment,
        IStrategy<CreateProductModel> insertProductStrategy)
    {
        _productServices = productServices;
        _categoryServices = categoryServices;
        _environment = environment;
        _insertProductStrategy = insertProductStrategy;
    }
    
    public async Task<ProductManagementViewModel> GetProductManagementViewModel(string? searchQuery, string? selectedCategory, int page, int pageSize)
    {
        var productResult = string.IsNullOrEmpty(selectedCategory)
            ? await _productServices.GetAllProducts()
            : await _productServices.SearchProductsByCategory(selectedCategory);

        if (!productResult.Success || productResult.Data == null)
            return new ProductManagementViewModel();

        var categoryResult = await _categoryServices.GetAllCategories();
        var lowStock = await _productServices.GetLowStokData();
        var inactiveStock = await _productServices.GetInactiveStokData();

        var filtered = SearchHelper.FilterByQuery(productResult.Data, searchQuery, p => new[]
        {
            p.ProductName,
            p.Brand,
            p.ProductCategory
        });

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
    
    public async Task<ServiceResult<CreateProductModel>> GetCreateProductViewModel(CreateProductModel model)
    {
        if (model == null)
        {
            return new ServiceResult<CreateProductModel>
            {
                Success = false,
                Data = null,
                ErrorMessage = "Invalid form submission."
            };
        }

        var categoryResult = await _categoryServices.GetAllCategories();
        model.Categories = categoryResult.Data?.Select(c => c.CategoryName).ToList();
        
        if (model.ProductImage != null)
        {
            model.ImageUrl = await ImageHelper.SaveProductImageAsync(model.ProductImage, _environment.WebRootPath);
        }

        var result = await _insertProductStrategy.CreateItem(model);

        return new ServiceResult<CreateProductModel>
        {
            Success = result.Success,
            Data = model,
            ErrorMessage = result.ErrorMessage
        };
    }
}

