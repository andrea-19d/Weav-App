using System.Text.Json;
using Weav_App.Models;

public class CartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CartKey = "Cart";

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private List<ProductModel.CartItem> Cart
    {
        get
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartKey);
            return string.IsNullOrEmpty(cartJson)
                ? new List<ProductModel.CartItem>()
                : JsonSerializer.Deserialize<List<ProductModel.CartItem>>(cartJson) ?? new List<ProductModel.CartItem>();
        }
        set
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString(CartKey, JsonSerializer.Serialize(value));
        }
    }

    public List<ProductModel.CartItem> GetCartItems() => Cart;

    public void AddToCart(int productId, string productName, decimal productPrice)
    {
        var cart = Cart;
        var item = cart.FirstOrDefault(c => c.ProductId == productId);
        if (item != null)
        {
            item.Quantity++;
        }
        else
        {
            cart.Add(new ProductModel.CartItem
            {
                ProductId = productId,
                ProductName = productName,
                ProductPrice = productPrice,
                Quantity = 1
            });
        }
        Cart = cart;
    }

    public void RemoveFromCart(int productId)
    {
        var cart = Cart;
        var item = cart.FirstOrDefault(c => c.ProductId == productId);
        if (item != null)
        {
            cart.Remove(item);
        }
        Cart = cart;
    }

    public decimal GetTotal()
    {
        return Cart.Sum(i => i.ProductPrice * i.Quantity);
    }
}