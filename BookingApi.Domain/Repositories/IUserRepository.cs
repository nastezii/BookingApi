using BookingApi.Domain.Entities;

namespace BookingApi.Domain.Repositories;

public interface IUserRepository
{
    bool ExistsByEmail(string email);

    void Add(User user);

    User? GetByEmailAndPassword(string email, string password);
}