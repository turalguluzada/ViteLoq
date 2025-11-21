using ViteLoq.Application.DTOs.Auth;

namespace ViteLoq.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<TokenResponseDto> LoginAsync(LoginDto dto, CancellationToken ct = default);
    Task<TokenResponseDto?> RefreshAsync(RefreshRequestDto dto, CancellationToken ct = default);
    Task RevokeAsync(string refreshToken, Guid userId, CancellationToken ct = default);
}