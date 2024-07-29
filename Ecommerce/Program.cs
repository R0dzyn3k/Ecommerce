using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var secretKey = builder.Configuration["Jwt:Key"];
        var services = builder.Services;

        services.AddDbContext<EcommerceDbContext>(x =>
        {
            x.UseSqlite(builder.Configuration.GetConnectionString("CString"));
        });

        services.AddAuthenticationJwtBearer(x => x.SigningKey = secretKey)
            .AddAuthorization();

        services.AddFastEndpoints()
            .AddHttpContextAccessor()
            .SwaggerDocument(x =>
            {
                x.EnableJWTBearerAuth = true;
                x.AutoTagPathSegmentIndex = 0;
            });

        var app = builder.Build();

        app.UseAuthentication()
            .UseAuthorization()
            .UseFastEndpoints(x => { x.Serializer.Options.Converters.Add(new JsonStringEnumConverter()); })
            .UseSwaggerGen();

        using (var scope = app.Services.CreateScope())
        {
            scope.ServiceProvider
                .GetRequiredService<EcommerceDbContext>()
                .Database
                .Migrate();
        }

        app.Run();
    }
}