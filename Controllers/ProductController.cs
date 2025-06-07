using Microsoft.AspNetCore.Mvc;
using Weav_App.DTOs.Entities.Products;
using Weav_App.Services.Interface;

namespace Weav_App.Controllers.Filters;

public class ProductController : Controller
{
    private readonly IProductServices _productServices;

    public ProductController(IProductServices productServices)
    {
        _productServices = productServices;
    }

    public IActionResult Index()
    {
        return View();
    }
}