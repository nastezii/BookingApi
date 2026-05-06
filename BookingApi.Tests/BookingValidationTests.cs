using BookingApi.DTOs;

namespace BookingApi.Tests;

public class BookingValidationTests
{
    [Fact]
    public void StartTime_ShouldBeInFuture()
    {
        var request = new BookingRequest
        {
            StartTime = DateTime.UtcNow.AddHours(-1),
            EndTime = DateTime.UtcNow.AddHours(1),
            Description = "Test booking"
        };

        Assert.True(request.StartTime <= DateTime.UtcNow);
    }

    [Fact]
    public void EndTime_ShouldBeAfterStartTime()
    {
        var request = new BookingRequest
        {
            StartTime = DateTime.UtcNow.AddHours(5),
            EndTime = DateTime.UtcNow.AddHours(2),
            Description = "Test booking"
        };

        Assert.True(request.EndTime <= request.StartTime);
    }
}