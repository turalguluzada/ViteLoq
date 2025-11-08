namespace ViteLoq.Application.DTOs.UserManagement;

public class AppUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string? UserName { get; set; }
    public string? DisplayName { get; set; }
}