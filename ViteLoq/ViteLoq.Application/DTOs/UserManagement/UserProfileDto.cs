namespace ViteLoq.Application.DTOs.UserManagement;

public class UserProfileDto
{
    public Guid UserId { get; set; }

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