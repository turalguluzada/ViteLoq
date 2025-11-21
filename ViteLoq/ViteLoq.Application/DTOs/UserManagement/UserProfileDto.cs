using ViteLoq.Domain.Entities;

namespace ViteLoq.Application.DTOs.UserManagement;

public class UserProfileDto
{
    public Guid UserId { get; set; }

    public string Email { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool EmailConfirmed { get; set; }
    public UserDetailDto? Detail { get; set; }

    // auth and security related (read-only)
    public string[] Roles { get; set; } = Array.Empty<string>();
    public UserClaimDto[] Claims { get; set; } = Array.Empty<UserClaimDto>();

    public DateTime CreatedDate { get; set; }
    public DateTime? LastSeenAt { get; set; } // optional if you track presence
    
    
    public string? DisplayName { get; set; }

    public double? HeightCm { get; set; }

    public double? WeightKg { get; set; }

    public DateTime? BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? ActivityLevel { get; set; }

    public string? KnownConditions { get; set; }
    public int? RestingHeartRate { get; set; }
    public string? BloodPressure { get; set; }
    
    
}