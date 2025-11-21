namespace ViteLoq.Application.DTOs.Auth;

public class LoginDto
{
    public string EmailOrUserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? IpAddress { get; set; } // optional, controller can pass
}