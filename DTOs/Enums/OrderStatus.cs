using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[JsonConverter(typeof(StringEnumConverter))]
public enum OrderStatus
{
    [EnumMember(Value = "cancelled")]
    Cancelled = 0,
    
    [EnumMember(Value = "pending")]
    Pending = 1,
    
    [EnumMember(Value = "confirmed")]
    Confirmed = 2,
    

    [EnumMember(Value = "shipped")]
    Shipped = 3,

    [EnumMember(Value = "arrived")]
    Arrived = 4,

    [EnumMember(Value = "completed")]
    Completed = 5
}