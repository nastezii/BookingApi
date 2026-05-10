using BookingApi.Domain.Entities;
using BookingApi.Domain.ValueObjects;
using BookingApi.Infrastructure.Entities;

namespace BookingApi.Infrastructure.Mappers;

public static class BookingMapper
{
    public static BookingEntity ToEntity(Booking booking)
    {
        return new BookingEntity
        {
            UserId = booking.UserId,
            StartTime = booking.TimeRange.Start,
            EndTime = booking.TimeRange.End,
            Description = booking.Description
        };
    }

    public static Booking ToDomain(BookingEntity entity)
    {
        return new Booking(
            entity.UserId,
            new TimeRange(entity.StartTime, entity.EndTime),
            entity.Description
        );
    }
}