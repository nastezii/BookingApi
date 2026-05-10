namespace BookingApi.Domain.Entities;

public class User
{
    public int Id { get; }

    public string Email { get; }

    public string PasswordHash { get; }

    public User(
        int id,
        string email,
        string passwordHash)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
    }
}