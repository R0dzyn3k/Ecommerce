using Ecommerce.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Orders.Update;

public class UpdateOrderCommandHandler(EcommerceDbContext dbContext) : Endpoint<UpdateOrderCommand, OrderModel>
{
    public override void Configure()
    {
        Post("orders/{Id}");
        Options(x => x.WithTags("Orders"));

        Summary(summary => { summary.Summary = "Update existing order"; });
    }

    public override async Task HandleAsync(UpdateOrderCommand req, CancellationToken ct)
    {
        var order = await dbContext.Orders.FirstAsync(x => x.Id == req.Id, ct);
            
        order.OrderStatus = Enum.Parse<OrderStatus>(req.OrderStatus);
        order.RealizationAt = req.RealizationAt;
        order.Email = req.Email;
        order.HasCustomerNote = req.HasCustomerNote;
        order.CustomerNote = req.CustomerNote;
        order.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);

        Response = OrderModel.FromEntity(order);
    }
}