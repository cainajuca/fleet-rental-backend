using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Infra.Database;
public class AppDbContext : DbContext
{
    public DbSet<AppUser> AppUser { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
