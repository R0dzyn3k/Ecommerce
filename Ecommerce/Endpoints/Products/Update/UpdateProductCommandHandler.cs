using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Products.Update;

public class UpdateProductCommandHandler(EcommerceDbContext dbContext) : Endpoint<UpdateProductCommand, ProductModel>
{
    public override void Configure()
    {
        Post("products/{Id}");
        Options(x => x.WithTags("Products"));

        Summary(summary => { summary.Summary = "Update existing product"; });
    }

    public override async Task HandleAsync(UpdateProductCommand req, CancellationToken ct)
    {
        var product = await dbContext.Products.FirstAsync(x => x.Id == req.Id, ct);

        product.Title = req.Title;
        product.DescriptionShort = req.DescriptionShort;
        product.Price = req.Price;
        product.Sku = req.Sku;
        product.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);

        Response = ProductModel.FromEntity(product);
    }
}