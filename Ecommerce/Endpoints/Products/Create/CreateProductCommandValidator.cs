using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Products.Create;

public class CreateProductCommandValidator : Validator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(255);

        RuleFor(x => x.DescriptionShort)
            .MaximumLength(512);

        RuleFor(x => x.Price)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Sku)
            .NotNull()
            .MaximumLength(255)
            .MustAsync(IsSkuUnique)
            .WithMessage("The order with the given SKU already exist.");
    }

    private async Task<bool> IsSkuUnique(string sku, CancellationToken ct)
    {
        using var scope = CreateScope();
        var dbContext = scope.Resolve<EcommerceDbContext>();

        var exists = await dbContext.Products.AnyAsync(x => x.Sku == sku, ct);

        return !exists;
    }
}