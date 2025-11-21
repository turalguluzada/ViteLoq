namespace ViteLoq.Application.DTOs.UserManagement;

public class ChangePasswordDto
{
    // Caller (controller) should fill UserId from token or route
    public Guid UserId { get; set; }

    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    // optional for client-side check
    public string? ConfirmNewPassword { get; set; }
}