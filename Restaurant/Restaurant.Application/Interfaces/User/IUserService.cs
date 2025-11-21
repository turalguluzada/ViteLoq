using Restaurant.Domain.Card.Entities;
using Restaurant.Domain.User.Entities;

namespace Restaurant.Application.Interfaces.User;

public interface IUserService
{
    public void AddUser();
    public void DeleteUser(AppUser appUser);
    public void UpdateUser(AppUser appUser);
    public AppUser GetUser();
    public void ShowUserCards(AppUser appUser);
    public List<Domain.Card.Entities.BankCard> GetUserCards(AppUser appUser);

    public void GetUserDetail(AppUser appUser);
    public void GetCards(AppUser appUser);
    public void AddCard(AppUser appUser, Domain.Card.Entities.BankCard bankCard);
    public void DeleteCard(AppUser appUser, Domain.Card.Entities.BankCard bankCard);
    
    public void AddDetails(AppUser appUser, UserDetail userDetail);
    public void DeleteDetails(AppUser appUser);
    public AppUser GetUser(Guid userId);
}