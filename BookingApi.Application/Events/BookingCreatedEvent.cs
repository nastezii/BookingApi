namespace BookingApi.Application.Events;

public class BookingCreatedEvent
{
    public int UserId { get; set; }

    public string Description { get; set; } = string.Empty;
}