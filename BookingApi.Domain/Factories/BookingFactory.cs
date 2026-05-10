using BookingApi.Domain.Entities;
using BookingApi.Domain.Errors;
using BookingApi.Domain.ValueObjects;

namespace BookingApi.Domain.Factories;

public static class BookingFactory
{
    public static Booking Create(
        int userId,
        DateTime start,
        DateTime end,
        string description)
    {
        if (start <= DateTime.UtcNow)
            throw new DomainError("Start time invalid");

        var range = new TimeRange(start, end);

        return new Booking(
            userId,
            range,
            description);
    }
}