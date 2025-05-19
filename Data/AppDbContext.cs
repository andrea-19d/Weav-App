using Microsoft.EntityFrameworkCore;
using Weav_App.Models;

namespace Weav_App.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<UserModel> Users { get; set; }
}