using Microsoft.AspNetCore.Mvc;
using Weav_App.DTOs.Entities.Products;
using Weav_App.Helpers;
using Weav_App.Services.Interface;

namespace Weav_App.Controllers.Filters;

public class ProductController : Controller
{
    private readonly IProductServices _productServices;
    private readonly IWebHostEnvironment _environment;

    public ProductController(IProductServices productServices, IWebHostEnvironment environment)
    {
        _productServices = productServices;
        _environment = environment;
    }

    public async Task<IActionResult> DeleteProducts(int id)
    {
        var result =await _productServices.DeleteProduct(id);

        if (!result.Status)
        {
            TempData["ErrorMessage"] = result.Message;
        }
        TempData["SuccessMessage"] = result.Message;
        return RedirectToAction("ProductManagement", "Admin");
    }
    
    public async Task<IActionResult> UpdateProduct(ProductDto product)
    {
        string existingImageUrl = product.ImageUrl;
        
        if (string.IsNullOrEmpty(product.ImageUrl))
        {
            product.ImageUrl = await ImageHelper.SaveProductImageAsync(product.ProductImage, _environment.WebRootPath);
        }
        else if (product.ProductImage != null && product.ProductImage.Length > 0)
        {
            product.ImageUrl = await ImageHelper.SaveProductImageAsync(product.ProductImage, _environment.WebRootPath);
        }
        else
        {
            product.ImageUrl = product.ImageUrl;
        }


        var result = await _productServices.UpdateProduct(product);
        if (!result.Status)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("ProductManagement", "Admin");
        }
        
        TempData["SuccessMessage"] = result.Message;
        return RedirectToAction("ProductManagement", "Admin");
    }
}