using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Orders.Show;

public class ShowOrderQueryHandler(EcommerceDbContext dbContext) : Endpoint<ShowOrderQuery, OrderModel>
{
    public override void Configure()
    {
        Get("orders/{Id}");
        Options(x => x.WithTags("Orders"));

        Summary(summary => { summary.Summary = "Get single order"; });
    }

    public override async Task HandleAsync(ShowOrderQuery req, CancellationToken ct)
    {
        var order = await dbContext.Orders.FirstAsync(x => x.Id == req.Id, ct);

        Response = OrderModel.FromEntity(order);
    }
}