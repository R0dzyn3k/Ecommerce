using System.Net;
using System.Net.Http.Headers;
using Ecommerce.Endpoints.Orders.Create;
using Ecommerce.Entities;
using Ecommerce.Tests.Extensions;
using Ecommerce.Tests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ecommerce.Tests.Tests;

public class OrderControllerTests(EcommerceApplicationFactory factory) : TestBase(factory)
{
    private readonly IConfiguration _configuration = factory.Services.GetRequiredService<IConfiguration>();

    [Fact]
    public async Task CreateOrderTest_Ok()
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var request = new CreateOrderCommand
        {
            Email = "test@example.com",
            HasCustomerNote = true,
            CustomerNote = "Please deliver after 5 PM"
        };

        // Act
        var response = await HttpClient.PostAsync("orders", request.ToJsonContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Assert_Database(db =>
        {
            db.Orders.Should().ContainSingle()
                .Which.Should().BeEquivalentTo(new
                {
                    request.Email,
                    request.HasCustomerNote,
                    request.CustomerNote
                });
        });
    }

    [Fact]
    public async Task GetOrderByIdTest_Ok()
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var order = Arrange_Database(x => x.Arrange_Order());

        // Act
        var response = await HttpClient.GetAsync($"orders/{order.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        Assert_Database(db =>
        {
            db.Orders.Should().ContainSingle()
                .Which.Should().BeEquivalentTo(new
                {
                    order.Email,
                    order.HasCustomerNote,
                    order.CustomerNote
                });
        });
    }

    [Fact]
    public async Task GetAllOrdersTest_Ok()
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var entities = Arrange_Database(x =>
        {
            var orders = new List<Order>
            {
                x.Arrange_Order(),
                x.Arrange_Order()
            };

            return orders;
        });

        // Act
        var response = await HttpClient.GetAsync("orders");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var orders = await response.Content.ReadAsAsync<List<Order>>();

        orders.Should().BeEquivalentTo(entities,
            options => options.Excluding(x => x.CreatedAt).Excluding(x => x.UpdatedAt));
        orders.Count.Should().Be(2);
    }

    [Fact]
    public async Task DeleteOrderTest_Ok()
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var order = Arrange_Database(x => x.Arrange_Order());

        // Act
        var response = await HttpClient.DeleteAsync($"orders/{order.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        Assert_Database(db => db.Orders.Should().BeEmpty());
    }

    [Fact]
    public async Task DeleteOrderTest_BadRequest()
    {
        // Arrange
        var token = JwtTokenHelper.GenerateJwtToken("root", "Administrator", _configuration);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await HttpClient.DeleteAsync("orders/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}