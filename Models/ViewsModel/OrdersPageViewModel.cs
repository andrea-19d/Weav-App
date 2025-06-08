using Weav_App.DTOs.Entities.Orders;
using Weav_App.Models.Orders;

namespace Weav_App.Models.ViewsModel;

public class OrdersPageViewModel : GeneralPageViewModel
{
    public List<OrdersListDTO>? Orders { get; set; }
    public OrdersFilterModel Filter { get; set; } = new();
    public OrderStats Stats { get; set; } = new();
}