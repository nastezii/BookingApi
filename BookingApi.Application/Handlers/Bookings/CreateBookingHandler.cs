using BookingApi.Application.Commands.Bookings;
using BookingApi.Domain.Factories;
using BookingApi.Domain.Repositories;

namespace BookingApi.Application.Handlers.Bookings;

public class CreateBookingHandler
{
    private readonly IBookingRepository _repository;

    public CreateBookingHandler(
        IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
     CreateBookingCommand command)
    {
        var hasConflict =
            _repository.HasConflict(
                command.Start,
                command.End);

        if (hasConflict)
        {
            throw new Exception(
                "Booking conflict");
        }

        var booking =
            BookingFactory.Create(
                command.UserId,
                command.Start,
                command.End,
                command.Description);

        await _repository.AddAsync(booking);
    }
}