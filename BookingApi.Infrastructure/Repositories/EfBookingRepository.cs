using BookingApi.Domain.Entities;
using BookingApi.Domain.Repositories;
using BookingApi.Infrastructure.Data;
using BookingApi.Infrastructure.Mappers;

namespace BookingApi.Infrastructure.Repositories;

public class EfBookingRepository : IBookingRepository
{
    private readonly AppDbContext _db;

    public EfBookingRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Booking booking)
    {
        var entity = BookingMapper.ToEntity(booking);

        _db.Bookings.Add(entity);

        await _db.SaveChangesAsync();
    }

    public async Task<List<Booking>> GetAllAsync()
    {
        return _db.Bookings
            .Select(BookingMapper.ToDomain)
            .ToList();
    }

    public bool HasConflict(DateTime start, DateTime end)
    {
        return _db.Bookings.Any(x =>
            start < x.EndTime &&
            end > x.StartTime);
    }

    public List<Booking> GetAllByUserId(int userId)
    {
        return _db.Bookings
            .Where(x => x.UserId == userId)
            .Select(BookingMapper.ToDomain)
            .ToList();
    }
}