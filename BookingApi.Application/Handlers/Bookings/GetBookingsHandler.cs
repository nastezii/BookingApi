using BookingApi.Application.Queries.Bookings;
using BookingApi.Application.ReadModels.Bookings;
using BookingApi.Domain.Repositories;

namespace BookingApi.Application.Handlers.Bookings;

public class GetBookingsHandler
{
    private readonly IBookingRepository _repository;

    public GetBookingsHandler(
        IBookingRepository repository)
    {
        _repository = repository;
    }

    public Task<List<BookingReadModel>> Handle(
        GetBookingsQuery query)
    {
        var bookings =
            _repository.GetAllByUserId(0);

        var result = bookings
            .Select(x => new BookingReadModel
            {
                UserId = x.UserId,
                Start = x.TimeRange.Start,
                End = x.TimeRange.End,
                Description = x.Description
            })
            .ToList();

        return Task.FromResult(result);
    }
}