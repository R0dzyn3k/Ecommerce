using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Products.Delete;

public class DeleteProductCommandHandler(EcommerceDbContext dbContext) : Endpoint<DeleteProductCommand, NoContent>
{
    public override void Configure()
    {
        Delete("products/{Id}");
        Options(x => x.WithTags("Products"));

        Summary(summary => { summary.Summary = "Deletes product by id"; });
    }

    public override async Task HandleAsync(DeleteProductCommand req, CancellationToken ct)
    {
        var product = await dbContext.Products.FirstAsync(x => x.Id == req.Id, ct);

        dbContext.Remove(product);
        await dbContext.SaveChangesAsync(ct);
    }
}