using Ecommerce.Enums;
using FastEndpoints;
using Order = Ecommerce.Entities.Order;

namespace Ecommerce.Endpoints.Orders.Create;

public class CreateOrderCommandHandler(EcommerceDbContext dbContext): Endpoint<CreateOrderCommand, OrderModel>
{
    public override void Configure()
    {
        Post("orders");
        Options(x => x.WithTags("Orders"));

        Summary(summary => { summary.Summary = "Creates new order"; });
    }

    public override async Task HandleAsync(CreateOrderCommand req, CancellationToken ct)
    {
        var order = new Order
        {
            OrderStatus = OrderStatus.New,
            RealizationAt = null,
            Email = req.Email,
            HasCustomerNote = req.HasCustomerNote,
            CustomerNote = req.CustomerNote,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await dbContext.Orders.AddAsync(order, ct);
        await dbContext.SaveChangesAsync(ct);

        Response = OrderModel.FromEntity(order);
    }
}