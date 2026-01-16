using BookingApp.Core.Entities;

namespace BookingApp.Core.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}
