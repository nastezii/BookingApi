using BookingApi.Application.EventHandlers;
using BookingApi.Application.Events;

namespace BookingApi.Application.EventBus;

public class InMemoryEventBus : IEventBus
{
    private readonly BookingCreatedEventHandler
        _bookingCreatedHandler;

    public InMemoryEventBus(
        BookingCreatedEventHandler
            bookingCreatedHandler)
    {
        _bookingCreatedHandler =
            bookingCreatedHandler;
    }

    public async Task PublishAsync<T>(T @event)
    {
        if (@event is BookingCreatedEvent bookingEvent)
        {
            await _bookingCreatedHandler
                .Handle(bookingEvent);
        }
    }
}