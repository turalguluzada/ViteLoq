using ViteLoq.Domain.Base.Entities;

namespace ViteLoq.Domain.UserTotal.Entities;

public class UserTotalSkin : BaseEntity
{
    // public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal SkinPoint { get; set; }
    public decimal HairPoint { get; set; }
    public decimal NailsPoint { get; set; }
}