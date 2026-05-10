using BookingApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserEntity> Users => Set<UserEntity>();

    public DbSet<BookingEntity> Bookings => Set<BookingEntity>();
}