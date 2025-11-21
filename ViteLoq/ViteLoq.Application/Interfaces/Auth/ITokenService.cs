using System.Security.Claims;

namespace ViteLoq.Application.Interfaces.Auth;

public interface ITokenService
{
    string CreateAccessToken(IEnumerable<Claim> claims, out DateTime expiresAt);
    (string token, DateTime expiresAt) GenerateRefreshToken();
    string HashToken(string token); // for storing in DB
}