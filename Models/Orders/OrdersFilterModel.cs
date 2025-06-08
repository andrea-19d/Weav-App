namespace Weav_App.Models.Orders;

public class OrdersFilterModel
{
    public string? SearchQuery { get; set; }
    public string? SelectedStatus { get; set; }
    public string? Status { get; set; }
    public DateOnly? DateFrom { get; set; }
    public string? Priority { get; set; }
}