using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Orders.Index;

public class IndexOrderQueryHandler(EcommerceDbContext dbContext) : EndpointWithoutRequest<List<OrderModel>>
{
    public override void Configure()
    {
        Get("orders");
        Options(x => x.WithTags("Orders"));

        Summary(summary => { summary.Summary = "Get all orders"; });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response = await dbContext
            .Orders
            .Select(OrderModel.MapFromEntityExpression)
            .ToListAsync(ct);
    }
}