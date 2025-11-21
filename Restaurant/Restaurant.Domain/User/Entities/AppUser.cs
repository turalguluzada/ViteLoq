namespace Restaurant.Domain.User.Entities;

public class AppUser
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid UserDetailId { get; set; }
    public List<Guid> CardsId { get; set; } = new List<Guid>();
}