using BookingApi.Application.Commands.Bookings;
using BookingApi.Application.Handlers.Bookings;
using BookingApi.Domain.Entities;
using BookingApi.Domain.Repositories;
using BookingApi.Domain.ValueObjects;
using Moq;

namespace BookingApi.Tests;

public class CreateBookingHandlerTests
{
    [Fact]
    public async Task Handle_ShouldThrowException_WhenBookingConflicts()
    {
        var repositoryMock =
            new Mock<IBookingRepository>();

        var existingBookings =
            new List<Booking>
            {
                new Booking(
                    1,
                    new TimeRange(
                        DateTime.Now,
                        DateTime.Now.AddHours(2)),
                    "Existing")
            };

        repositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(existingBookings);

        var handler =
            new CreateBookingHandler(
                repositoryMock.Object);

        var command =
            new CreateBookingCommand
            {
                UserId = 1,
                Start = DateTime.Now.AddMinutes(30),
                End = DateTime.Now.AddHours(1),
                Description = "Test"
            };

        await Assert.ThrowsAsync<Exception>(() =>
            handler.Handle(command));
    }
}