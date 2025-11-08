using ViteLoq.Domain.Base.Entities;
using ViteLoq.Domain.Templates.Entities;

namespace ViteLoq.Domain.UserEntry.Entities;

public class UserNutritionEntry : BaseEntity    
{
    // public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid NutritionId { get; set; }
    public decimal Grams { get; set; }
    // public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    // public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
   
}