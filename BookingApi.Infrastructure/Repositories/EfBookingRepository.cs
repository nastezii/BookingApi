using BookingApi.Domain.Entities;
using BookingApi.Domain.Repositories;
using BookingApi.Infrastructure.Data;
using BookingApi.Infrastructure.Mappers;

namespace BookingApi.Infrastructure.Repositories;

public class EfUserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public EfUserRepository(AppDbContext db)
    {
        _db = db;
    }

    public bool ExistsByEmail(string email)
    {
        return _db.Users.Any(u => u.Email == email);
    }

    public void Add(User user)
    {
        var entity = UserMapper.ToEntity(user);

        _db.Users.Add(entity);

        _db.SaveChanges();
    }

    public User? GetByEmailAndPassword(
        string email,
        string password)
    {
        var entity = _db.Users.FirstOrDefault(u =>
            u.Email == email &&
            u.PasswordHash == password);

        if (entity == null)
            return null;

        return UserMapper.ToDomain(entity);
    }
}