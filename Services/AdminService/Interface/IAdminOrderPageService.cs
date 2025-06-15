using Microsoft.AspNetCore.Mvc;
using Weav_App.DTOs;
using Weav_App.Models.ViewsModel;

namespace Weav_App.Services.Interface;

public interface IAdminOrderPageService
{
   Task<OrdersPageViewModel> GetOrdersPageViewModel(string? searchQuery, OrderStatus? selectedStatus, int page,
        int pageSize);
}