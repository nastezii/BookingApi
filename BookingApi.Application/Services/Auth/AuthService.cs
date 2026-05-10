using BookingApi.Domain.Repositories;
using BookingApi.Domain.Entities;
using BookingApi.Domain.Errors;
using BookingApi.Application.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookingApi.Application.Services.Auth;

public class AuthService
{
    private readonly IUserRepository _repository;

    private readonly string _key = "key_for_jwt_123456";

    public AuthService(IUserRepository repository)
    {
        _repository = repository;
    }

    public void Register(RegisterRequest request)
    {
        if (!request.Email.Contains('@'))
            throw new DomainError("Invalid email");

        if (_repository.ExistsByEmail(request.Email))
            throw new DomainError("Email exists");

        var user = new User(
            0,
            request.Email,
            request.Password);

        _repository.Add(user);
    }

    public string Login(LoginRequest request)
    {
        var user = _repository.GetByEmailAndPassword(
            request.Email,
            request.Password);

        if (user == null)
            throw new DomainError("Invalid credentials");

        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_key));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}