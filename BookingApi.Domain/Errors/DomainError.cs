namespace BookingApi.Domain.Errors;

public class DomainError : Exception
{
    public DomainError(string message)
        : base(message)
    {
    }
}