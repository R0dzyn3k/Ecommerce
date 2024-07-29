using Ecommerce.Entities;
using FastEndpoints;

namespace Ecommerce.Endpoints.Products.Create;

public class CreateProductCommandHandler(EcommerceDbContext dbContext) : Endpoint<CreateProductCommand, ProductModel>
{
    public override void Configure()
    {
        Post("products");
        Options(x => x.WithTags("Products"));

        Summary(summary => { summary.Summary = "Creates new product"; });
    }

    public override async Task HandleAsync(CreateProductCommand req, CancellationToken ct)
    {
        var product = new Product
        {
            Title = req.Title,
            DescriptionShort = req.DescriptionShort,
            Price = req.Price,
            Sku = req.Sku,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await dbContext.Products.AddAsync(product, ct);
        await dbContext.SaveChangesAsync(ct);

        Response = ProductModel.FromEntity(product);
    }
}
