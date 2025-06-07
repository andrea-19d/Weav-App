using System.ComponentModel.DataAnnotations;
using Postgrest.Attributes;
using Postgrest.Models;

namespace Weav_App.DTOs.Entities.User
{
    [Table("Users")]
    public class UserDbTable : BaseModel
    {
        [PrimaryKey("id", false)]
        public int UserId { get; set; }

        [Column("UserName")]
        public string Username { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Password")]
        public string PasswordHash { get; set; }

        [Column("UserIP")]
        public string UserIp { get; set; }

        [Column("CreatedAt")]
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

        [Column("LastLogin")]
        public DateTime? LastLogin { get; set; }

        [Column("UserPhoto")]
        public byte[]? UserPhoto { get; set; } 
        
        [Column("Level")]
        public UserLevel Level { get; set; }
    }
}