namespace ViteLoq.Application.DTOs.Auth;

public class RefreshRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
    public string? IpAddress { get; set; }
}