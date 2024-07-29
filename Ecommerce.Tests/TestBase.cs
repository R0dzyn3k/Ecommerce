using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ecommerce.Tests;

public class TestBase : IClassFixture<EcommerceApplicationFactory>
{
    private readonly EcommerceApplicationFactory _factory;
    protected readonly HttpClient HttpClient;

    protected TestBase(EcommerceApplicationFactory factory)
    {
        _factory = factory;
        HttpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        CreateDatabase();
    }

    protected T Arrange_Database<T>(Func<EcommerceDbContext, T> arrange)
    {
        using var scope = _factory.Services.CreateScope();
        var scopedService = scope.ServiceProvider;
        using var db = scopedService.GetRequiredService<EcommerceDbContext>();

        var result = arrange.Invoke(db);
        db.SaveChanges();

        return result;
    }

    protected void Arrange_Database(Action<EcommerceDbContext> arrange)
    {
        using var scope = _factory.Services.CreateScope();
        var scopedService = scope.ServiceProvider;
        using var db = scopedService.GetRequiredService<EcommerceDbContext>();

        arrange.Invoke(db);
        db.SaveChanges();
    }

    protected void Assert_Database(Action<EcommerceDbContext> assert)
    {
        using var scope = _factory.Services.CreateScope();
        var scopedService = scope.ServiceProvider;
        using var db = scopedService.GetRequiredService<EcommerceDbContext>();

        assert.Invoke(db);
    }

    private void CreateDatabase()
    {
        using var scope = _factory.Services.CreateScope();
        var scopedService = scope.ServiceProvider;
        var db = scopedService.GetRequiredService<EcommerceDbContext>();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

}