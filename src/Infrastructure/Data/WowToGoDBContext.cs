using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class WowToGoDBContext(DbContextOptions<WowToGoDBContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("wowtogo");
    }
}