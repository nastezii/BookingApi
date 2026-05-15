using BookingApi.Application.Commands.Bookings;
using BookingApi.Application.Contracts;
using BookingApi.Application.Handlers.Bookings;
using BookingApi.Application.Queries.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingApi.Controllers;

[ApiController]
[Route("bookings")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly CreateBookingHandler _createHandler;

    private readonly GetBookingsHandler _getHandler;

    public BookingsController(
        CreateBookingHandler createHandler,
        GetBookingsHandler getHandler)
    {
        _createHandler = createHandler;

        _getHandler = getHandler;
    }

    private Guid GetUserId()
    {
        return Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetBookingsQuery();

        var bookings =
            await _getHandler.Handle(query);

        return Ok(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        BookingRequest request)
    {
        var command = new CreateBookingCommand
        {
            Start = request.StartTime,
            End = request.EndTime,
            UserId = GetUserId()
        };

        var id =
            await _createHandler.Handle(command);

        return Ok(id);
    }
}