using BookingApi.Application.Commands.Bookings;
using BookingApi.Application.EventBus;
using BookingApi.Application.Events;
using BookingApi.Domain.Factories;
using BookingApi.Domain.Repositories;

namespace BookingApi.Application.Handlers.Bookings;

public class CreateBookingHandler
{
    private readonly IBookingRepository
        _repository;

    private readonly IEventBus
        _eventBus;

    public CreateBookingHandler(
        IBookingRepository repository,
        IEventBus eventBus)
    {
        _repository = repository;

        _eventBus = eventBus;
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

        var bookingCreatedEvent =
            new BookingCreatedEvent
            {
                UserId = command.UserId,
                Description =
                    command.Description
            };

        await _eventBus.PublishAsync(
            bookingCreatedEvent);
    }
}