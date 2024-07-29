using System.Net;
using System.Text.Json;
using Ecommerce.Endpoints.Administrators;
using Ecommerce.Endpoints.Administrators.Login;
using Ecommerce.Tests.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ecommerce.Tests.Tests;

public class AuthControllerTests(EcommerceApplicationFactory factory) : TestBase(factory)
{
    private readonly IConfiguration _configuration = factory.Services.GetRequiredService<IConfiguration>();

    [Fact]
    public async Task LoginAdministratorTest_Ok()
    {
        // Arrange
        var admin = Arrange_Database(x => x.Arrange_Administrator());
        var loginCommand = new LoginAdministratorCommand
        {
            Username = admin.Username,
            Password = admin.Password
        };

        // Act
        var response = await HttpClient.PostAsync("auth/login", loginCommand.ToJsonContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await response.Content.ReadAsStringAsync();

        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        tokenResponse.Should().NotBeNull();
        tokenResponse?.AccessToken.Should().NotBeNullOrEmpty();
    }


    [Fact]
    public async Task LoginAdministratorTest_UserNotFound()
    {
        // Arrange
        var loginCommand = new LoginAdministratorCommand
        {
            Username = "nonexistent",
            Password = "password"
        };

        // Act
        var response = await HttpClient.PostAsync("auth/login", loginCommand.ToJsonContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}