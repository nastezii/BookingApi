using BookingApi.Application.EventBus;
using BookingApi.Application.EventHandlers;
using BookingApi.Application.Handlers.Bookings;
using BookingApi.Application.Notifications;
using BookingApi.Domain.Repositories;
using BookingApi.Infrastructure.Data;
using BookingApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<CreateBookingHandler>();

builder.Services.AddScoped<GetBookingsHandler>();

builder.Services.AddScoped<
    IBookingRepository,
    EfBookingRepository>();

builder.Services.AddScoped<
    INotificationService,
    NotificationService>();

builder.Services.AddScoped<
    BookingCreatedEventHandler>();

builder.Services.AddScoped<
    IEventBus,
    InMemoryEventBus>();

var key = "key_for_jwt_123456";

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key))
            };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}