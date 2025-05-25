using System.Collections.Generic;
using System.Linq;
using Weav_App.Models;

public class CartService
{
    private readonly List<CartItem> _cart = new();

    public IEnumerable<CartItem> GetCartItems() => _cart;

    public void AddToCart(int productId, string productName, decimal productPrice)
    {
        var existingItem = _cart.FirstOrDefault(ci => ci.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            _cart.Add(new CartItem
            {
                ProductId = productId,
                ProductName = productName,
                ProductPrice = productPrice,
                Quantity = 1
            });
        }
    }

    public void RemoveFromCart(int productId)
    {
        var item = _cart.FirstOrDefault(ci => ci.ProductId == productId);
        if (item != null)
        {
            _cart.Remove(item);
        }
    }

    public decimal GetTotal() => _cart.Sum(ci => ci.ProductPrice * ci.Quantity);
}