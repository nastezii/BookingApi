namespace BookingApi.DTOs;

public class BookingRequest
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Description { get; set; } = "";
}