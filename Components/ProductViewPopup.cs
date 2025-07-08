using Microsoft.AspNetCore.Mvc;
using Weav_App.Services.Interface;

namespace Weav_App.Components;

public class ProductViewPopup : ViewComponent
{
    private readonly IProductServices _productServices;

    public ProductViewPopup(IProductServices productServices)
    {
        _productServices = productServices;
    }

    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var productResult = await _productServices.GetProductByIdAsync(id);

        if (!productResult.Success)
        {
            TempData["ErrorMessage"] = productResult.ErrorMessage;
        }
        
        return View(productResult.Data);
    }


}