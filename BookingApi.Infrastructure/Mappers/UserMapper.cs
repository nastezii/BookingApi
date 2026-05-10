using BookingApi.Domain.Entities;
using BookingApi.Infrastructure.Entities;

namespace BookingApi.Infrastructure.Mappers;

public static class UserMapper
{
    public static UserEntity ToEntity(User user)
    {
        return new UserEntity
        {
            Id = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash
        };
    }

    public static User ToDomain(UserEntity entity)
    {
        return new User(
            entity.Id,
            entity.Email,
            entity.PasswordHash);
    }
}