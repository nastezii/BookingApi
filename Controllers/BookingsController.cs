using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BookingApi.Data;
using BookingApi.DTOs;
using BookingApi.Models;
using System.Security.Claims;

namespace BookingApi.Controllers;

[ApiController]
[Route("bookings")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _db;

    public BookingsController(AppDbContext db)
    {
        _db = db;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var userId = GetUserId();
        var bookings = _db.Bookings.Where(b => b.UserId == userId).ToList();
        return Ok(bookings);
    }

    [HttpPost]
    public IActionResult Create(BookingRequest request)
    {
        if (request.StartTime <= DateTime.UtcNow)
            return BadRequest("Start time in past");

        if (request.EndTime <= request.StartTime)
            return BadRequest("Invalid range");

        var conflict = _db.Bookings.Any(b =>
            b.StartTime < request.EndTime &&
            b.EndTime > request.StartTime);

        if (conflict)
            return Conflict("Time conflict");

        var booking = new Booking
        {
            UserId = GetUserId(),
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Description = request.Description
        };

        _db.Bookings.Add(booking);
        _db.SaveChanges();

        return Ok(booking);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BookingRequest request)
    {
        var booking = _db.Bookings.Find(id);
        if (booking == null)
            return NotFound();

        if (request.StartTime <= DateTime.UtcNow)
            return BadRequest();

        if (request.EndTime <= request.StartTime)
            return BadRequest();

        var conflict = _db.Bookings.Any(b =>
            b.Id != id &&
            b.StartTime < request.EndTime &&
            b.EndTime > request.StartTime);

        if (conflict)
            return Conflict();

        booking.StartTime = request.StartTime;
        booking.EndTime = request.EndTime;
        booking.Description = request.Description;

        _db.SaveChanges();

        return Ok(booking);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var booking = _db.Bookings.Find(id);
        if (booking == null)
            return NotFound();

        _db.Bookings.Remove(booking);
        _db.SaveChanges();

        return Ok();
    }
}