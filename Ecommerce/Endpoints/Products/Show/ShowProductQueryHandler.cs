using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Products.Show;

public class ShowProductQueryHandler(EcommerceDbContext dbContext) : Endpoint<ShowProductQuery, ProductModel>
{
    public override void Configure()
    {
        Get("products/{Id}");
        Options(x => x.WithTags("Products"));

        Summary(summary => { summary.Summary = "Get single product"; });
    }

    public override async Task HandleAsync(ShowProductQuery req, CancellationToken ct)
    {
        var product = await dbContext.Products.FirstAsync(x => x.Id == req.Id, ct);

        Response = ProductModel.FromEntity(product);
    }
}