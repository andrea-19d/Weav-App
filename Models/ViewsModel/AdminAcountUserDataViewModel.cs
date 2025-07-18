using Weav_App.Models.Orders;

namespace Weav_App.Models.ViewsModel;

public class AdminAcountUserDataViewModel
{
    public UserAccountData UserAccount { get; set; } = new();
    public OrderStats OrderData { get; set; } = new();
    public ProductStats ProductData { get; set; } = new();
}