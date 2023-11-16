using APIWeb.Domain.City;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<City> Cities { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<City>()
            .Property(c => c.Name).IsRequired().HasMaxLength(50);
        
        builder.Entity<City>()
            .Property(c => c.State).IsRequired().HasMaxLength(24);
    }
}
