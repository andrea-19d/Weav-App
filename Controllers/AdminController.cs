using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Weav_App.Controllers.Filters;
using Weav_App.DTOs;
using Weav_App.DTOs.Entities.Categories;
using Weav_App.DTOs.Entities.Products;
using Weav_App.Helpers.HelpersUI;
using Weav_App.Models;
using Weav_App.Models.ViewsModel;
using Weav_App.Services.General;
using Weav_App.Services.Interface;

namespace Weav_App.Controllers;

[UserLevelAuthorize(UserLevel.Admin)]
public class AdminController : Controller
{
    
    private readonly IMapper _mapper;
    private readonly IProductServices _productServices;
    private readonly ICategoryServices _categoryServices;
    

    public AdminController(IProductServices productServices, ICategoryServices categoryServices, IMapper mapper)
    {
        _productServices = productServices;
        _categoryServices = categoryServices;
        _mapper = mapper;
    }
    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult AdminAccount()
    {
        return View();
    }
    
    // [HttpGet("Admin/Products/category/{categoryName}")]
    public async Task<IActionResult> ProductManagement(string? searchQuery, string? selectedCategory, int page = 1, int pageSize = 10)
    {
        ServiceResult<List<ProductDto>> productResult;
        Console.WriteLine($"category: {selectedCategory}");

        if (!string.IsNullOrEmpty(selectedCategory))
        {
            productResult = await _productServices.SearchProductsByCategory(selectedCategory);
        }
        else
        {
            productResult = await _productServices.GetAllProducts();
        }
        
        var categoryResult = await _categoryServices.GetAllCategories();
        var lowStakResult = await _productServices.GetLowStokData();
        var inactiveStakResult = await _productServices.GetInactiveStokData();
            Console.WriteLine($"Total Products from DB: {productResult.Data.Count}");
        

        
        
        Console.WriteLine($"get all products{productResult}");
        Console.WriteLine($"get all categories{categoryResult.Data}");

        if (!productResult.Success || productResult.Data == null)
        {
            ViewBag.Error = productResult.ErrorMessage ?? "Unknown error";
            return View(new ProductManagementViewModel());
        }

        // Search
        var filtered = SearchHelper.FilterByQuery(productResult.Data, searchQuery, p =>
        [
            p.ProductName,
            p.Brand,
            p.ProductCategory
        ]);
        
        if (!string.IsNullOrEmpty(selectedCategory))
        {
            filtered = filtered.Where(p => p.ProductCategory == selectedCategory).ToList();
        }

        // Pagination
        var paginated = PaginationHelper.Paginate(filtered, page, pageSize);

        ViewBag.CurrentPage = paginated.CurrentPage;
        ViewBag.TotalPages = paginated.TotalPages;
        ViewBag.SearchQuery = searchQuery;
        ViewBag.Products = productResult.Data.Count;
        ViewBag.Category = categoryResult.Data.Count;
        ViewBag.LowStock = lowStakResult.Data.Count;
        ViewBag.Inactive = inactiveStakResult.Data.Count;

        // Return ViewModel
        var viewModel = new ProductManagementViewModel
        {
            Products = paginated.Items,
            Filter = new ProductFilterModel
            {
                SearchQuery = searchQuery,
                SelectedCategory = selectedCategory,
                Category = categoryResult.Data.Select(c => c.CategoryName).ToList()
            }
        };

        return View(viewModel);
    }




    public IActionResult OrderPage()
    {
        return View();
    }
    
}