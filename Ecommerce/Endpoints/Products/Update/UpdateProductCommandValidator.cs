using System.Net;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Products.Update;

public class UpdateProductCommandValidator : Validator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(IsProductExist)
            .WithMessage("The product with the given ID does not exist.")
            .WithErrorCode(HttpStatusCode.BadRequest.ToString());

        RuleFor(x => x.Title)
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(255);

        RuleFor(x => x.DescriptionShort)
            .MaximumLength(255);

        RuleFor(x => x.Price)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Sku)
            .NotNull()
            .MaximumLength(255)
            .MustAsync((model, sku, ct) => IsSkuUnique(model.Id, sku, ct))
            .WithMessage("Produkt o podanym sku już istnieje");
    }

    private async Task<bool> IsSkuUnique(int id, string sku, CancellationToken ct)
    {
        using var scope = CreateScope();
        var dbContext = scope.Resolve<EcommerceDbContext>();

        var exists = await dbContext.Products.AnyAsync(x => x.Sku == sku  && x.Id != id, ct);

        return !exists;
    }

    private async Task<bool> IsProductExist(int id, CancellationToken ct)
    {
        using var scope = CreateScope();
        var dbContext = scope.Resolve<EcommerceDbContext>();

        return await dbContext.Products.AnyAsync(x => x.Id == id, ct);
    }
}