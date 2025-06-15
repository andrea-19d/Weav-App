using Microsoft.AspNetCore.Mvc;
using Weav_App.Models.ViewsModel;
using Weav_App.Services.Interface;

namespace Weav_App.Components;

public class QuickActionsViewComponent : ViewComponent
{
    private readonly IOrderService _orderService;

    public QuickActionsViewComponent(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var pendingOrders = await _orderService.GetAllPendingOrders();

        var model = new QuickActionsViewModel
        {
            NumberOfPendingOrders = pendingOrders.Data?.Count ?? 0
        };

        return View("QuickActions", model);
    }   
}