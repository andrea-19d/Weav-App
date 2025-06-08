using Postgrest.Attributes;
using Postgrest.Models;

namespace Weav_App.DTOs.Entities.Categories;

[Table("Category")]
public class CategoryDbTable : BaseModel
{
    [Column("CategoryId")] 
    public int CategoryId { get; set; }

    [Column("CategoryName")] 
    public string CategoryName { get; set; }

    [Column("Description")] 
    public string Description { get; set; }
}