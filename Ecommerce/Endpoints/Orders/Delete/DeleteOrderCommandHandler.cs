using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Orders.Delete;

public class DeleteOrderCommandHandler(EcommerceDbContext dbContext) : Endpoint<DeleteOrderCommand, NoContent>
{
    public override void Configure()
    {
        Delete("orders/{Id}");
        Options(x => x.WithTags("Orders"));

        Summary(summary => { summary.Summary = "Deletes order by id"; });
    }

    public override async Task HandleAsync(DeleteOrderCommand req, CancellationToken ct)
    {
        var order = await dbContext.Orders.FirstAsync(x => x.Id == req.Id, ct);

        dbContext.Remove(order);
        await dbContext.SaveChangesAsync(ct);
    }
}