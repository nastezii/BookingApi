using BookingApi.Application.Queries.Bookings;
using BookingApi.Application.ReadModels.Bookings;
using BookingApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Application.Handlers.Bookings;

public class GetBookingsHandler
{
    private readonly AppDbContext _context;

    public GetBookingsHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookingReadModel>> Handle(GetBookingsQuery query)
    {
        return await _context.Bookings
            .Select(x => new BookingReadModel
            {
                Id = x.Id,
                Start = x.Start,
                End = x.End,
                UserId = x.UserId
            })
            .ToListAsync();
    }
}