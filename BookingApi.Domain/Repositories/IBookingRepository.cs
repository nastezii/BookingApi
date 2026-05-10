using BookingApi.Domain.Entities;

namespace BookingApi.Domain.Repositories;

public interface IBookingRepository
{
    void Add(Booking booking);

    bool HasConflict(DateTime start, DateTime end);

    List<Booking> GetAllByUserId(int userId);
}