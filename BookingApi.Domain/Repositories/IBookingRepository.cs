using BookingApi.Domain.Entities;

namespace BookingApi.Domain.Repositories;

public interface IBookingRepository
{
    Task AddAsync(Booking booking);

    Task<List<Booking>> GetAllAsync();

    bool HasConflict(DateTime start, DateTime end);

    List<Booking> GetAllByUserId(int userId);
}