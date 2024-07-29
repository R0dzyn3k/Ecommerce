using Ecommerce.Entities;
using Ecommerce.Enums;

namespace Ecommerce.Tests.Extensions;

public static class ArrangeExtensions
{
    public static Product Arrange_Product(this EcommerceDbContext dbContext, string title = "Title-t1",
        decimal price = 30,
        string descriptionShort = "test", string sku = "Sku-t1")
    {
        var product = new Product
        {
            Title = title,
            Price = price,
            DescriptionShort = descriptionShort,
            Sku = sku
        };

        dbContext.Add(product);

        return product;
    }

    public static Administrator Arrange_Administrator(this EcommerceDbContext dbContext, string username = "admin",
        string password = "password", AdministratorRole role = AdministratorRole.Administrator,
        string email = "admin@example.com", string firstName = "John", string lastName = "Doe",
        string phoneNumber = "+48 123 456 789")
    {
        var administrator = new Administrator
        {
            Username = username,
            Password = password,
            Role = role,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber
        };

        dbContext.Add(administrator);
        dbContext.SaveChanges();

        return administrator;
    }

    public static Order Arrange_Order(this EcommerceDbContext dbContext, OrderStatus status = OrderStatus.New,
        string email = "customer@example.com", bool hasCustomerNote = false, string? customerNote = null,
        DateTime? realizationAt = null)
    {
        var order = new Order
        {
            OrderStatus = status,
            RealizationAt = realizationAt,
            Email = email,
            HasCustomerNote = hasCustomerNote,
            CustomerNote = customerNote,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dbContext.Add(order);
        dbContext.SaveChanges();

        return order;
    }
}