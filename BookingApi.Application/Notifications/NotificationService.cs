namespace BookingApi.Application.Notifications;

public class NotificationService
    : INotificationService
{
    public Task SendAsync(string message)
    {
        Console.WriteLine(
            $"NOTIFICATION: {message}");

        return Task.CompletedTask;
    }
}
