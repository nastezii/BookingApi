namespace BookingApi.Application.Notifications;

public interface INotificationService
{
    Task SendAsync(string message);
}