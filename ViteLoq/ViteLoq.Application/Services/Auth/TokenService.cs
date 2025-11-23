using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ViteLoq.Application.Interfaces.Auth;

namespace ViteLoq.Application.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _cfg;
    private readonly byte[] _signingKey;

    public TokenService(IConfiguration cfg)
    {
        _cfg = cfg;
        var key = _cfg["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key missing");
        _signingKey = Encoding.UTF8.GetBytes(key);
    }

    public string CreateAccessToken(IEnumerable<Claim> claims, out DateTime expiresAt)
    {
        var issuer = _cfg["Jwt:Issuer"];
        var audience = _cfg["Jwt:Audience"];
        var minutes = int.Parse(_cfg["Jwt:AccessTokenMinutes"] ?? "15");
        expiresAt = DateTime.UtcNow.AddMinutes(minutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_signingKey), SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    

    public (string token, DateTime expiresAt) GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        RandomNumberGenerator.Fill(randomBytes);
        var token = Convert.ToBase64String(randomBytes);
        var days = int.Parse(_cfg["Jwt:RefreshTokenDays"] ?? "30");
        var expires = DateTime.UtcNow.AddDays(days);
        return (token, expires);
    }

    public string HashToken(string token)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}