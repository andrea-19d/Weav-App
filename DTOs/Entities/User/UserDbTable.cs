using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Weav_App.DTOs.Entities.User
{
    [Table("Users")]
    public class UserDbTable : BaseModel
    {
        [PrimaryKey("id", false)]
        public int UserId { get; set; }
        
        [Column("FirstName")]
        public string FirstName { get; set; }
        
        [Column("LastName")]
        public string LastName { get; set; }
        
        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }
        
        [Column("userName")]
        public string Username { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Password")]
        public string PasswordHash { get; set; }

        [Column("UserIP")]
        public string UserIp { get; set; }

        [Column("createdAt")]
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

        [Column("LastLogin")]
        public DateTime? LastLogin { get; set; }

        [Column("UserPhoto")]
        public byte[]? UserPhoto { get; set; } 
        
        [Column("Level")]
        public UserLevel Level { get; set; }
    }
}