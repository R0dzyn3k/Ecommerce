using Ecommerce.Enums;
using FastEndpoints;
using FluentValidation;

namespace Ecommerce.Endpoints.Orders.Create;

public class CreateOrderCommandValidator : Validator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
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
}