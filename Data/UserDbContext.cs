using Microsoft.EntityFrameworkCore;
using Weav_App.DTOs.Entities.User;
using Weav_App.Models;

namespace Weav_App.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<UserDbTable> Users { get; set; }
}