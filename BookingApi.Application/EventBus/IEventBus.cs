namespace BookingApi.Application.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T @event);
}