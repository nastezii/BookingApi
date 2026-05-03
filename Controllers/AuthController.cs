using Microsoft.AspNetCore.Mvc;
using BookingApi.Data;
using BookingApi.DTOs;
using BookingApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookingApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly string _key = "12345";

    public AuthController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        if (!request.Email.Contains("@"))
            return BadRequest("Invalid email");

        if (_db.Users.Any(u => u.Email == request.Email))
            return Conflict("Email exists");

        var user = new User
        {
            Email = request.Email,
            PasswordHash = request.Password 
        };

        _db.Users.Add(user);
        _db.SaveChanges();

        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var user = _db.Users
            .FirstOrDefault(u => u.Email == request.Email && u.PasswordHash == request.Password);

        if (user == null)
            return Unauthorized();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}