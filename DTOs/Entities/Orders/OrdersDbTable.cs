using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using Weav_App.DTOs.Entities.User;
using Newtonsoft.Json;


namespace Weav_App.DTOs.Entities.Orders;

[Table("Orders")]
public class OrdersDbTable : BaseModel
{
    [PrimaryKey("OrderId", false)]
    [Column("OrderId")]
    public int OrderId { get; set; }
    
    [Column("PurchaseOrder")]
    public string PurchaseOrder { get; set; }
    
    [Column("UserId")]
    public int UserId { get; set; }
    
    [Column("CreatedAt")]
    public DateTime OrderDate { get; set; }
    
    [Column("TotalAmount")]
    public decimal TotalAmount { get; set; }
    
    [Column("Status")]
    public short StatusValue { get; set; }
    
    [Column("StreetName")]
    public string StreetName { get; set; }
    
    [Column("StreetAddress")]
    public string StreetAddress { get; set; }
    
    [Column("PhoneNumber")]
    public string PhoneNumber { get; set; }
    
    [Column("City")]
    public string City { get; set; }
    
    [Column("Comments")]
    public string Comments { get; set; }
    
    [JsonIgnore] 
    public OrderStatus Status
    {
        get => (OrderStatus)StatusValue;
        set => StatusValue = (short)value;
    }
    
    [JsonIgnore]
    public List<OrderItemDbTable> Items { get; set; } = new();

    [JsonIgnore]
    public UserDbTable User { get; set; }

    
}