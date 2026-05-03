namespace BookingApi.Models;

public class Booking
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string Description { get; set; } = "";
}