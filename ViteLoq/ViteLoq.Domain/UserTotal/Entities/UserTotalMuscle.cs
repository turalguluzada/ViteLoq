using ViteLoq.Domain.Base.Entities;

namespace ViteLoq.Domain.UserTotal.Entities;

public class UserTotalMuscle : BaseEntity
{
    // public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Chest { get; set; }
    public decimal Back { get; set; }
    public decimal Biceps { get; set; }
    public decimal Triceps { get; set; }
    public decimal Quadriceps { get; set; }
    public decimal Hamstrings { get; set; }
    public decimal Calves { get; set; }
    public decimal Shoulders { get; set; }
    public decimal Abs { get; set; }
    public decimal Glutes { get; set; }
    
}