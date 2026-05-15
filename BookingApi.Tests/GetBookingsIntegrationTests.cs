using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BookingApi.Tests;

public class GetBookingsIntegrationTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GetBookingsIntegrationTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetBookings_ShouldReturnSuccessStatusCode()
    {
        var response =
            await _client.GetAsync("/bookings");

        Assert.Equal(
            HttpStatusCode.Unauthorized,
            response.StatusCode);
    }
}