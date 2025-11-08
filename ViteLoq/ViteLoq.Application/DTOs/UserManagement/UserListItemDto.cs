namespace ViteLoq.Application.DTOs.UserManagement;

public class UserListItemDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string? DisplayName { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();
}