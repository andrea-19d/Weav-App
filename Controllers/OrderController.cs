using Microsoft.AspNetCore.Mvc;
using Weav_App.Models.ViewsModel;
using Weav_App.Repositories.Interface;
using Weav_App.Services.Interface;

namespace Weav_App.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> ConfirmOrder(int orderId)
    {
       Console.WriteLine($"Order id: {orderId}");
       
        var result = await _orderService.ChangeOrderStatus(orderId);
        
        if (!result.Status)
        {
            TempData["ErrorMessage"] = result.Message;
        }
        else
        {
            TempData["SuccessMessage"] = "Order confirmed successfully.";
        }

        return RedirectToAction("NewOrderPage", "Order"); 
    }
    
    [HttpGet]
    public async Task<IActionResult> NewOrderPage()
    {
        var result = await _orderService.GetAllPendingOrders();

        if (!result.Success)  // or result.Status == false
        {
            TempData["ErrorMessage"] = result.ErrorMessage ?? "Failed to load orders.";
            return View(new OrdersPageViewModel()); // return empty view model
        }

        var viewModel = new OrdersPageViewModel
        {
            Orders = result.Data // Extract the actual list from the ServiceResult
        };

        return View(viewModel);
    }
    
   
}