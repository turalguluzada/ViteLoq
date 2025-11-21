using ViteLoq.Domain.Base.Entities;

namespace ViteLoq.Domain.Entities;

public class UserDetail : BaseEntity
{
    // public Guid Id { get; set; }
    public Guid AppUserId { get; set; }    // PK = Identity user id
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }       // "Male","Female","Other" - enum tercihi olabilir
    public decimal HeightCm { get; set; }   // decimal(6,2)
    public decimal WeightKg { get; set; }   // decimal(6,2)
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
}