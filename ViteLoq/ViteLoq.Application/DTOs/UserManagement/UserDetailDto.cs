namespace ViteLoq.Application.DTOs.UserManagement;

public class UserDetailDto
{
    public Guid UserId { get; set; }

    /// <summary>Height in centimeters</summary>
    public double? HeightCm { get; set; }

    /// <summary>Weight in kilograms</summary>
    public double? WeightKg { get; set; }

    public DateTime? BirthDate { get; set; }

    /// <summary>Male/Female/Other - free string or enum mapping in service</summary>
    public string? Gender { get; set; }

    /// <summary>Activity level: Sedentary/Light/Moderate/Active/VeryActive or numeric code</summary>
    public string? ActivityLevel { get; set; }

    // optional health fields
    public string? KnownConditions { get; set; }
    public int? RestingHeartRate { get; set; }
    public string? BloodPressure { get; set; }

    // timestamps
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}