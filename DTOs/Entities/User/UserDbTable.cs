using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weav_App.DTOs.Entities.User
{
    public class UserDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password Hash")]
        [MaxLength(200)] // store the hashed password
        public string PasswordHash { get; set; }

        [MaxLength(45)] // IP address (IPv6 max length is 45)
        public string UserIP { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? LastLogin { get; set; }

        public byte[]? UserPhoto { get; set; } 
        
        public UserLevel Level { get; set; }
    }
}