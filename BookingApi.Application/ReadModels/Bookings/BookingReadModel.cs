namespace BookingApi.Application.ReadModels.Bookings;

public class BookingReadModel
{
    public int UserId { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string Description { get; set; } = "";
}