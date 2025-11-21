namespace Restaurant.Domain.Tables.Entities;

public class Table
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid ReservedUserId { get; set; }
    public bool IsReserved { get; set; }
    public int Cost { get; set; }
}