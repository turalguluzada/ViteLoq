using ViteLoq.Domain.Base.Entities;

namespace ViteLoq.Domain.UserTotal.Entities;

public class UserTotalHealth : BaseEntity
{
    // public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Heart { get; set; }
    public decimal Kidney { get; set; }
    public decimal Liver { get; set; }
    public decimal Lungs { get; set; }
    public decimal Eyes { get; set; }
    public decimal Brain { get; set; }
    public decimal Pancreas { get; set; }
    public decimal Stomach { get; set; }
    // public string HealthName { get; set; }
    // public string HealthPoint { get; set; }
}