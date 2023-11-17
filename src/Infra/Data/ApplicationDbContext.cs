using APIWeb.Domain.City;
using APIWeb.Domain.Client;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<Client> Clients { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<City>()
            .Property(c => c.Name).IsRequired().HasMaxLength(50);
        
        builder.Entity<City>()
            .Property(c => c.State).IsRequired().HasMaxLength(24);

        builder.Entity<Client>()
            .Property(c => c.Name).IsRequired().HasMaxLength(150);
        builder.Entity<Client>()
            .Property(c => c.Sexo).HasMaxLength(20);

        builder.Entity<Client>()
            .HasOne(c => c.City)
            .WithMany(c => c.Clients)
            .HasForeignKey(c => c.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
