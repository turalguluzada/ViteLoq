using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ViteLoq.Application.DTOs.Auth;
using ViteLoq.Application.Interfaces.Auth;
using ViteLoq.Domain.Auth.Entities;
using ViteLoq.Domain.Interfaces;
using ViteLoq.Domain.Token.Interfaces;
using ViteLoq.Domain.UserManagement.Interfaces;

namespace ViteLoq.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRefreshTokenRepository _refreshRepo;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _uow; // to save refresh token

        public AuthService(IUserRepository userRepo, IRefreshTokenRepository refreshRepo, ITokenService tokenService,
            IUnitOfWork uow)
        {
            _userRepo = userRepo;
            _refreshRepo = refreshRepo;
            _tokenService = tokenService;
            _uow = uow;
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto dto, CancellationToken ct = default)
        {
            // find user by email or username
            var user = await _userRepo.FindByEmailOrUserNameAsync(dto.EmailOrUserName, ct)
                       ?? throw new InvalidOperationException("Invalid credentials");

            // check password
            var ok = await _userRepo.CheckPasswordAsync(user, dto.Password, ct);
            if (!ok) throw new InvalidOperationException("Invalid credentials");

            // build claims (sub, email, jti, roles)
            var roles = await _userRepo.GetRolesAsync(user, ct);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            // create access token
            var access = _tokenService.CreateAccessToken(claims, out var accessExp);

            // create refresh token
            var (refreshPlain, refreshExp) = _tokenService.GenerateRefreshToken();
            var refreshHash = _tokenService.HashToken(refreshPlain);

            var refreshEntity = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = refreshHash,
                ExpiresAt = refreshExp,
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = dto.IpAddress ?? "unknown"
            };

            await _refreshRepo.AddAsync(refreshEntity, ct);
            await _uow.SaveChangesAsync(ct);

            return new TokenResponseDto
            {
                AccessToken = access,
                AccessTokenExpiresAt = accessExp,
                RefreshToken = refreshPlain,
                RefreshTokenExpiresAt = refreshExp
            };
        }

        public async Task<TokenResponseDto?> RefreshAsync(RefreshRequestDto dto, CancellationToken ct = default)
        {
            // hash incoming token and look up
            var hash = _tokenService.HashToken(dto.RefreshToken);
            var stored = await _refreshRepo.GetByHashAsync(hash, ct);
            if (stored == null || stored.Revoked || stored.ExpiresAt <= DateTime.UtcNow)
                return null;

            // get user
            var user = await _userRepo.GetByIdAsync(stored.UserId, ct);
            if (user == null) return null;

            // rotate: revoke existing and create new refresh token
            stored.Revoked = true;
            stored.RevokedAt = DateTime.UtcNow;

            var (newPlain, newExp) = _tokenService.GenerateRefreshToken();
            var newHash = _tokenService.HashToken(newPlain);

            var newEntity = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = newHash,
                ExpiresAt = newExp,
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = dto.IpAddress ?? "unknown",
                ReplacedByTokenHash = null
            };

            // link replacement
            stored.ReplacedByTokenHash = newHash;

            await _refreshRepo.UpdateAsync(stored, ct);
            await _refreshRepo.AddAsync(newEntity, ct);
            await _uow.SaveChangesAsync(ct);

            // create new access token
            var roles = await _userRepo.GetRolesAsync(user, ct);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var access = _tokenService.CreateAccessToken(claims, out var accessExp);

            return new TokenResponseDto
            {
                AccessToken = access,
                AccessTokenExpiresAt = accessExp,
                RefreshToken = newPlain,
                RefreshTokenExpiresAt = newExp
            };
        }

        public async Task RevokeAsync(string refreshToken, Guid userId, CancellationToken ct = default)
        {
            var hash = _tokenService.HashToken(refreshToken);
            var stored = await _refreshRepo.GetByHashAsync(hash, ct);
            if (stored == null || stored.UserId != userId) return;
            stored.Revoked = true;
            stored.RevokedAt = DateTime.UtcNow;
            await _refreshRepo.UpdateAsync(stored, ct);
            await _uow.SaveChangesAsync(ct);
        }
    }
}