using System.Net;
using Ecommerce.Enums;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Orders.Update;

public class UpdateOrderCommandValidator : Validator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(IsOrderExist)
            .WithMessage("The order with the given ID does not exist.")
            .WithErrorCode(HttpStatusCode.BadRequest.ToString());

        RuleFor(x => x.OrderStatus)
            .NotEmpty()
            .Must(BeValidOrderStatus)
            .WithMessage("Invalid order status.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(254)
            .WithMessage("Invalid email address.");

        RuleFor(x => x.HasCustomerNote)
            .NotNull();

        RuleFor(x => x.CustomerNote)
            .MaximumLength(255)
            .When(x => x.HasCustomerNote)
            .WithMessage("Customer note cannot exceed 255 characters.");
    }

    private async Task<bool> IsOrderExist(int id, CancellationToken ct)
    {
        using var scope = CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();

        return await dbContext.Orders.AnyAsync(x => x.Id == id, ct);
    }

    private bool BeValidOrderStatus(string status)
    {
        return Enum.TryParse<OrderStatus>(status, true, out _);
    }
}