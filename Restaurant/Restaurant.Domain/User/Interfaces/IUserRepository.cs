using Restaurant.Domain.Card.Entities;
using Restaurant.Domain.User.Entities;

namespace Restaurant.Domain.User.Interfaces;

public interface IUserRepository
{
    public void AddUser(AppUser user);
    public void DeleteUser(Guid userId);
    public void UpdateUser(AppUser user);
    public AppUser GetUser(string username, string password);
    public List<BankCard> ShowUserCards(string username);
    public List<BankCard> GetUserCards(string username, string password);

    public AppUser FindUser(string username, string password);
    public AppUser FindUser(string username);
    public AppUser FindUser(string email, int value);
    public UserDetail GetUserDetail(string username, string password);
    public List<BankCard> GetCards(Guid userId);
    public void AddCard(AppUser user, Guid cardId);
    public void DeleteCard(AppUser user, Guid cardId);
    
    public void AddDetails(AppUser user, Guid userDetailId);
    public void DeleteDetails(AppUser user);
    public AppUser GetUser(Guid userId);

}