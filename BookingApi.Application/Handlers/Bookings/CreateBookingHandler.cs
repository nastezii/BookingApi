using BookingApi.Application.Commands.Bookings;
using BookingApi.Domain.Entities;
using BookingApi.Domain.Factories;
using BookingApi.Domain.Repositories;

namespace BookingApi.Application.Handlers.Bookings;

public class CreateBookingHandler
{
    private readonly IBookingRepository _repository;

    public CreateBookingHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateBookingCommand command)
    {
        var bookings = await _repository.GetAllAsync();

        var hasConflict = bookings.Any(x =>
            command.Start < x.End &&
            command.End > x.Start);

        if (hasConflict)
        {
            throw new Exception("Booking conflict");
        }

        var booking = BookingFactory.Create(
            command.Start,
            command.End,
            command.UserId);

        await _repository.AddAsync(booking);

        return booking.Id;
    }
}