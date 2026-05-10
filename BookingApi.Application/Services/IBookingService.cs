using BookingApi.Application.Contracts;
using BookingApi.Domain.Entities;

namespace BookingApi.Application.Services;

public interface IBookingService
{
    List<Booking> GetAll(int userId);

    Booking Create(int userId, BookingRequest request);
}