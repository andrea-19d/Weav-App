using Newtonsoft.Json;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using Weav_App.DTOs.Entities.Categories;

namespace Weav_App.DTOs.Entities.Products;

[Table("Products")] 
public class ProductDbTable : BaseModel
{
    [PrimaryKey("ProductId", true)] 
    [JsonIgnore]
    public int ProductId { get; set; }
    
    [Column("ProductName")] 
    public string ProductName { get; set; }
    
    [Column("Brand")]
    public string Brand { get; set; }
    
    [Column("ProductPrice")]
    public decimal ProductPrice { get; set; }
    
    [Column("Quantity")]
    public int Quantity { get; set; }
    
    [Column("Units")]
    public UnitsEnums Units { get; set; }
    
    [Column("ProductDescription")]
    public string? ProductDescription { get; set; }
    
    [Column("ImageUrl")]
    public string? ImageUrl { get; set; }
    
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; }
    
    [Column("UpdatedAt")]
    public DateTime UpdatedAt { get; set; }

    [Column("ExpiryDate")]
    public DateTime ExpiryDate { get; set; }
    
    [Column("Barcode")]
    public string Barcode { get; set; }
    
    [Column("CategoryId")]
    public int CategoryId { get; set; }
    
    
    [JsonIgnore]
    public CategoryDbTable? Category { get; set; }
}