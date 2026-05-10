using BookingApi.Domain.ValueObjects;

namespace BookingApi.Domain.Entities;

public class Booking
{
    public int UserId { get; }
    public TimeRange TimeRange { get; }
    public string Description { get; }

    public Booking(
        int userId,
        TimeRange timeRange,
        string description)
    {
        UserId = userId;
        TimeRange = timeRange;
        Description = description;
    }
}