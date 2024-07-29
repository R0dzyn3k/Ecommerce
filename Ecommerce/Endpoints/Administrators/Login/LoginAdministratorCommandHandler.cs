using Ecommerce.Entities;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Administrators.Login;

public class LoginAdministratorCommandHandler(EcommerceDbContext dbContext, IConfiguration config) : Endpoint<LoginAdministratorCommand, TokenResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post("auth/login");
        Options(x => x.WithTags("Auth"));
		
        Summary(x =>
        {
            x.Summary = "Loguje się na istniejące konto Administratora";
        });
    }

    public override async Task HandleAsync(LoginAdministratorCommand req, CancellationToken ct)
    {
        var administrator = await dbContext.Administrators.SingleAsync(x => x.Username == req.Username, ct);

        Response = new TokenResponse
        {
            AccessToken = CreateToken(administrator)
        };
    }

    private string CreateToken(Administrator administrator)
    {
        var jwtToken = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = config["Jwt:Key"]!;
                o.ExpireAt = DateTime.UtcNow.AddDays(1);
                o.User.Claims.Add(("UserName", administrator.Username));
                o.User["UserId"] = administrator.Id.ToString();
            });

        return jwtToken;
    }
}