using System.ComponentModel.DataAnnotations;

namespace ViteLoq.Application.DTOs.UserManagement;

public class CreateUserDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required, MinLength(8)]
    public string Password { get; set; } = null!;
    [Required, MinLength(8)]
    public string FirstName { get; set; } = null!;
    [Required, MinLength(8)]
    public string LastName { get; set; } = null!;

    // optional username; if omitted we'll use email (or map in service)
    [StringLength(100)]
    public string? UserName { get; set; }

    [Required, StringLength(100)]
    public string DisplayName { get; set; } = null!;
    
    // optional initial roles (e.g. "User")
    public IEnumerable<string>? Roles { get; set; }  
    
    // optional initial profile fields (can be null)
    public UserDetailDto? InitialProfile { get; set; }
}