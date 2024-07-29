using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Products.Index;

public class IndexProductQueryHandler(EcommerceDbContext dbContext) : EndpointWithoutRequest<List<ProductModel>>
{
    public override void Configure()
    {
        Get("products");
        Options(x => x.WithTags("Products"));

        Summary(summary => { summary.Summary = "Get all products"; });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response = await dbContext.Products
            .Select(ProductModel.MapFromEntityExpression)
            .ToListAsync(ct);
    }
}