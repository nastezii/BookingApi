namespace BookingApi.Application.Commands.Bookings;

public class CreateBookingCommand
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public int UserId { get; set; }

    public string Description { get; set; } = "";
}