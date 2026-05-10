using BookingApi.Application.Contracts;
using BookingApi.Application.Services;
using BookingApi.Domain.Entities;
using BookingApi.Domain.Errors;
using BookingApi.Domain.Repositories;
using BookingApi.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingApi.Controllers;

[ApiController]
[Route("bookings")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _service;

    public BookingsController(IBookingService service)
    {
        _service = service;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    [HttpGet]
    public IActionResult GetAll()
    {
        var bookings = _service.GetAll(GetUserId());

        return Ok(bookings);
    }

    [HttpPost]
    [HttpPost]
    public IActionResult Create(BookingRequest request)
    {
        try
        {
            var booking = _service.Create(
                GetUserId(),
                request);

            return Ok(booking);
        }
        catch (DomainError ex)
        {
            return BadRequest(ex.Message);
        }
    }
}