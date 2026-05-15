namespace BookingApi.Application.ReadModels.Bookings;

public class BookingReadModel
{
    public int Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public int UserId { get; set; }
}