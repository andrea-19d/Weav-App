using Weav_App.DTOs;
using Weav_App.DTOs.Entities.Orders;

namespace Weav_App.Models.Orders;

public class OrdersFilterModel
{
    public string? SearchQuery { get; set; }
    public OrderStatus? SelectedStatus { get; set; }
    public List<string>? Status { get; set; }
    public DateOnly? DateFrom { get; set; }
    public  List<string>? Priority { get; set; }
}