namespace BookingApi.Application.ReadModels.Bookings;

public class BookingReadModel
{
    public Guid Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public Guid UserId { get; set; }
}