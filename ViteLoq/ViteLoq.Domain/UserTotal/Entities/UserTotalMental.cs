using ViteLoq.Domain.Base.Entities;

namespace ViteLoq.Domain.UserTotal.Entities;

public class UserTotalMental : BaseEntity
{
    // public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Mood { get; set; }
    public decimal Stress { get; set; }
    public decimal Sleep { get; set; }
}