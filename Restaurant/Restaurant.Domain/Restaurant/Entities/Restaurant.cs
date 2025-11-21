namespace Restaurant.Domain.Restaurant.Entities;

public class Restaurant
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Guid> TableIds { get; set; } = new List<Guid>();
    public int PropertyValue { get; set; }
    public Guid UserId { get; set; }
}