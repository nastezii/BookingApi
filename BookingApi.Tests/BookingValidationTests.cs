using BookingApi.Application.Contracts;

namespace BookingApi.Tests;

public class BookingValidationTests
{
    [Fact]
    public void EndTime_ShouldBeAfterStartTime()
    {
        var request = new BookingRequest
        {
            StartTime = DateTime.UtcNow.AddHours(5),
            EndTime = DateTime.UtcNow.AddHours(2)
        };

        Assert.True(request.EndTime <= request.StartTime);
    }
}