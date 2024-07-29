using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Administrators.Login;

public class LoginAdministratorCommandValidator : Validator<LoginAdministratorCommand>
{
    public LoginAdministratorCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithName(x => x.Username)
            .MinimumLength(5)
            .WithName(x => x.Username)
            .MaximumLength(50)
            .WithName(x => x.Username);

        RuleFor(x => x)
            .MustAsync(BeCorrectCredentials)
            .WithName(x => x.Username)
            .WithMessage("Podany login i/lub hasło jest nieprawidłowe");
    }

    private async Task<bool> BeCorrectCredentials(LoginAdministratorCommand req, CancellationToken ct)
    {
        var context = Resolve<EcommerceDbContext>();

        var isCorrectCredentials = await context.Administrators
            .AnyAsync(a => a.Username == req.Username, ct);

        return isCorrectCredentials;
    }
}