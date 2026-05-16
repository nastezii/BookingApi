using BookingApi.Application.Events;
using BookingApi.Application.Notifications;

namespace BookingApi.Application.EventHandlers;

public class BookingCreatedEventHandler
{
    private readonly INotificationService
        _notificationService;

    public BookingCreatedEventHandler(
        INotificationService notificationService)
    {
        _notificationService =
            notificationService;
    }

    public async Task Handle(
        BookingCreatedEvent @event)
    {
        await _notificationService.SendAsync(
            $"ASYNC EVENT: booking created for user {@event.UserId}");
    }
}