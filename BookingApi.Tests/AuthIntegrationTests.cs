using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace BookingApi.Tests;

public class AuthIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_ShouldReturnOk()
    {
        var response = await _client.PostAsJsonAsync("/auth/register", new
        {
            email = "test@test.com",
            password = "12345"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}