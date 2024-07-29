using Ecommerce.Entities;
using Ecommerce.Enums;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce;

public class EcommerceDbContext(DbContextOptions<EcommerceDbContext> opt) : DbContext(opt)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Customer> Customers { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var typesToConfigure = typeof(EcommerceDbContext).GetProperties()
            .Where(x => x.PropertyType.Name == typeof(DbSet<>).Name)
            .Select(x => typeof(IEntityTypeConfiguration<>).MakeGenericType(x.PropertyType.GenericTypeArguments[0]));

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly,
            type => type.GetInterfaces().Any(i => typesToConfigure.Any(tc => tc == i))
        );

        modelBuilder.Entity<Administrator>().HasData(new Administrator
        {
            Id = 1,
            Username = "root",
            Password = "password",
            Role = AdministratorRole.Root,
            Email = "example@mail.pl",
            FirstName = "Adam",
            LastName = "Małysz",
            PhoneNumber = "+48 654 897 546"
        });
    }
}