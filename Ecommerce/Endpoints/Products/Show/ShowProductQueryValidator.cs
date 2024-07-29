using System.Net;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Products.Show;

public class ShowProductQueryValidator : Validator<ShowProductQuery>
{
    public ShowProductQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(IsProductExist)
            .WithMessage("The product with the given ID does not exist.")
            .WithErrorCode(HttpStatusCode.BadRequest.ToString());
    }

    private async Task<bool> IsProductExist(int id, CancellationToken ct)
    {
        using var scope = CreateScope();
        var dbContext = scope.Resolve<EcommerceDbContext>();

        return await dbContext.Products.AnyAsync(x => x.Id == id, ct);
    }
}