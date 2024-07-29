using System.Linq.Expressions;
using Ecommerce.Entities;

namespace Ecommerce.Endpoints.Products;

public class ProductModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string DescriptionShort { get; set; }
    public string Sku { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public static readonly Expression<Func<Product, ProductModel>> MapFromEntityExpression = product =>
        new ProductModel
        {
            Id = product.Id,
            Title = product.Title,
            DescriptionShort = product.DescriptionShort,
            Price = product.Price,
            Sku = product.Sku,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };

    public static ProductModel FromEntity(Product product)
    {
        return MapFromEntityExpression.Compile().Invoke(product);
    }
}