namespace ViteLoq.Application.DTOs.UserManagement;

public class UpdateProfileDto
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string? UserName { get; set; }

    public string? DisplayName { get; set; }

    // Identity flags
    public bool EmailConfirmed { get; set; }
    public bool LockoutEnabled { get; set; }
    public DateTime CreatedAt { get; set; }

    // Roles (string names)
    public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();

    // Claims (optional)
    public IEnumerable<UserClaimDto> Claims { get; set; } = Array.Empty<UserClaimDto>();

    // profile / detail
    public UserDetailDto? Detail { get; set; }
}