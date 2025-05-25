using Microsoft.AspNetCore.Mvc;
using Weav_App.Services;

namespace Weav_App.Controllers;

using Microsoft.AspNetCore.Mvc;

public class CartController : Controller
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    public IActionResult Index()
    {
        var cartItems = _cartService.GetCartItems();
        var total = _cartService.GetTotal();
        ViewBag.Total = total;
        return View(cartItems);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId, string productName, decimal productPrice)
    {
        _cartService.AddToCart(productId, productName, productPrice);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        _cartService.RemoveFromCart(productId);
        return RedirectToAction("Index");
    }
}
