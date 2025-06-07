using Weav_App.DTOs;

namespace Weav_App.Models;

public class ProductModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Brand { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
    public UnitsEnums Units { get; set; }
    public string? ProductDescription { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Barcode { get; set; }
    public int CategoryId { get; set; }

    public ProductModel(int productId, string productName, string brand, decimal productPrice, int quantity,
        UnitsEnums units, DateTime updatedAt, string productDescription, string imageUrl, DateTime createdAt, 
        DateTime expiryDate, string barcode, int categoryId)
    {
        ProductId = productId;
        ProductName = productName;
        Brand = brand;
        ProductPrice = productPrice;
        Quantity = quantity;
        Units = units;
        ProductDescription = productDescription;
        ImageUrl = imageUrl;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        ExpiryDate = expiryDate;
        Barcode = barcode;
        CategoryId = categoryId;
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductsList
    {
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string ProductCategory { get; set; }
        public string ProductPrice { get; set; }
        public int Quantity { get; set; } 
    }
    
}