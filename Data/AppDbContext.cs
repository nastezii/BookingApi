using BookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Booking> Bookings => Set<Booking>();
}