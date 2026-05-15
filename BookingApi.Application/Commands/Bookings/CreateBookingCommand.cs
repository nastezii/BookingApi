namespace BookingApi.Application.Commands.Bookings;

public class CreateBookingCommand
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public Guid UserId { get; set; }
}