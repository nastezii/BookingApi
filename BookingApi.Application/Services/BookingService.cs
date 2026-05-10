using BookingApi.Application.Contracts;
using BookingApi.Domain.Entities;
using BookingApi.Domain.Errors;
using BookingApi.Domain.Factories;
using BookingApi.Domain.Repositories;
using BookingApi.Domain.Errors;

namespace BookingApi.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;

    public BookingService(IBookingRepository repository)
    {
        _repository = repository;
    }

    public List<Booking> GetAll(int userId)
    {
        return _repository.GetAllByUserId(userId);
    }

    public Booking Create(int userId, BookingRequest request)
    {
        var hasConflict = _repository.HasConflict(
            request.StartTime,
            request.EndTime);

        if (hasConflict)
            throw new DomainError("Time conflict");

        var booking = BookingFactory.Create(
            userId,
            request.StartTime,
            request.EndTime,
            request.Description);

        _repository.Add(booking);

        return booking;
    }
}