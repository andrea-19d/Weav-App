using Weav_App.DTOs.Entities.Orders;
using Weav_App.DTOs.Entities.User;

namespace Weav_App.Helpers.Mapper;

public class MapOrdersToDTOsManual
{
    public List<OrdersListDTO> MapOrdersToDTOs(List<OrdersDbTable> orders, List<UserDbTable> users)
    {
        var userDict = users.ToDictionary(u => u.UserId);

        return orders.Select(order =>
        {
            var user = userDict.TryGetValue(order.UserId, out var u) ? u : null;
            return new OrdersListDTO
            {
                PurchaseOrder = order.PurchaseOrder,
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                StatusValue = order.StatusValue,    
                Status = order.Status,
                UserName = user?.Username ?? "N/A",
                Email = user?.Email ?? "N/A"
            };
        }).ToList();
    }
}