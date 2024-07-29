using System.Net;
using System.Net.Http.Headers;
using Ecommerce.Endpoints.Products.Create;
using Ecommerce.Endpoints.Products.Delete;
using Ecommerce.Entities;
using Ecommerce.Tests.Extensions;
using Ecommerce.Tests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ecommerce.Tests.Tests;

public class ProductControllerTests(EcommerceApplicationFactory factory) : TestBase(factory)
{
    private readonly IConfiguration _configuration = factory.Services.GetRequiredService<IConfiguration>();

    [Fact]
    public async Task CreateProductTest_Ok()
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new CreateProductCommand
        {
            Title = "Test 1",
            Price = 20,
            DescriptionShort = null,
            Sku = "Test_123"
        };

        // Act
        var response = await HttpClient.PostAsync("products", request.ToJsonContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Assert_Database(db =>
        {
            db.Products.Should().ContainSingle()
                .Which.Should().BeEquivalentTo(new
                {
                    request.Title,
                    request.Price,
                    request.DescriptionShort,
                    request.Sku
                });
        });
    }

    [Theory]
    [InlineData(["test1", -1, "null", "T1", HttpStatusCode.BadRequest])]
    public async Task CreateProductTest_BadRequest(string title, decimal price, string? desc, string sku,
        HttpStatusCode code)
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new CreateProductCommand
        {
            Title = title,
            Price = price,
            DescriptionShort = desc,
            Sku = sku
        };

        //Act
        var response = await HttpClient.PostAsync("products", request.ToJsonContent());

        //Assert
        response.StatusCode.Should().Be(code);
    }


    [Fact]
    public async Task DeleteProductTest_Ok()
    {
        //Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var entity = Arrange_Database(x => x.Arrange_Product());
        var request = new DeleteProductCommand
        {
            Id = entity.Id
        };

        //Act
        var response = await HttpClient.DeleteAsync($"products/{request.Id}", CancellationToken.None);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        Assert_Database(db => { db.Products.Should().BeEmpty(); });
    }


    [Theory]
    [InlineData([-1, HttpStatusCode.BadRequest])]
    [InlineData([null, HttpStatusCode.MethodNotAllowed])]
    [InlineData([30, HttpStatusCode.BadRequest])]
    public async Task DeleteProductTest_BadRequest(int? id, HttpStatusCode code)
    {
        //Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        Arrange_Database(x => x.Arrange_Product());

        //Act
        var response = await HttpClient.DeleteAsync($"products/{id}", CancellationToken.None);

        //Assert
        response.StatusCode.Should().Be(code);
    }


    [Fact]
    public async Task GetProductByIdTest_Ok()
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var entity = Arrange_Database(x => x.Arrange_Product());

        // Act
        var response = await HttpClient.GetAsync($"products/{entity.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var product = await response.Content.ReadAsAsync<Product>();
        product.Should().BeEquivalentTo(entity,
            options => options.Excluding(x => x.CreatedAt).Excluding(x => x.UpdatedAt));
    }

    [Theory]
    [InlineData(-1, HttpStatusCode.BadRequest)]
    [InlineData(999, HttpStatusCode.BadRequest)]
    public async Task GetProductByIdTest_BadRequest(int id, HttpStatusCode expectedStatusCode)
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await HttpClient.GetAsync($"products/{id}");

        // Assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    [Fact]
    public async Task GetAllProductsTest_Ok()
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var entities = Arrange_Database(x =>
        {
            var products = new List<Product>
            {
                x.Arrange_Product("Product 1", 10, "Description 1", "Sku1"),
                x.Arrange_Product("Product 2", 20, "Description 2", "Sku2")
            };
            return products;
        });

        // Act
        var response = await HttpClient.GetAsync("products");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var products = await response.Content.ReadAsAsync<List<Product>>();
        products.Should().BeEquivalentTo(entities,
            options => options.Excluding(x => x.CreatedAt).Excluding(x => x.UpdatedAt));
    }

    [Fact]
    public async Task GetAllProductsTest_Empty()
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await HttpClient.GetAsync("products");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var products = await response.Content.ReadAsAsync<List<Product>>();
        products.Should().BeEmpty();
    }
}