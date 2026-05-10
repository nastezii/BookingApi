using BookingApi.Domain.Errors;

namespace BookingApi.Domain.ValueObjects;

public class TimeRange
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public TimeRange(DateTime start, DateTime end)
    {
        if (end <= start)
            throw new DomainError("Invalid range");

        Start = start;
        End = end;
    }
}