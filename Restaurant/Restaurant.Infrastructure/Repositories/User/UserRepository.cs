using Restaurant.Domain.Card.Entities;
using Restaurant.Domain.User.Entities;
using Restaurant.Domain.User.Interfaces;

namespace Restaurant.Infrastructure.Repositories.User;

public class UserRepository : IUserRepository
{
    public List<AppUser> AppUsers = Context.AppDbContext.AppUsers; 
    public List<Domain.Card.Entities.BankCard> BankCards = Context.AppDbContext.BankCards;
    public List<UserDetail> UserDetails = Context.AppDbContext.UserDetails;

    public UserRepository()
    {
        if (Context.AppDbContext.AppUsers.Count < 1)
        {
            Context.AppDbContext.RefreshUserList();
            AppUsers = Context.AppDbContext.AppUsers;
        }
        if (Context.AppDbContext.BankCards.Count < 1)
        {
            Context.AppDbContext.RefreshBankCardList();
            BankCards = Context.AppDbContext.BankCards;
        }  
    }
    public void AddUser(AppUser user)
    {
        user.Id = Guid.NewGuid();
        Context.AppDbContext.AppUsers.Add(user);
        Context.AppDbContext.RefreshUserDb();
    }

    public void DeleteUser(Guid userId)
    {
        var dbUser = FindUser(userId);
        
        if (dbUser != null)
            dbUser.IsActive = false;
    }

    public void UpdateUser(AppUser user)
    {
        var dbUser = FindUser(user.Id);
        
        if (dbUser != null)
            dbUser = user;
        
        // maybe null
    }

    public AppUser GetUser(string username, string password)
    {
       var dbUser = FindUser(username, password);
       return dbUser;
    }

    public List<Domain.Card.Entities.BankCard> ShowUserCards(string username)
    {
        var dbUser = FindUser(username);
        return GetCards(dbUser.Id);
    }

    public List<Domain.Card.Entities.BankCard> GetUserCards(string username, string password)
    {
        var dbUser = FindUser(username, password);
        return GetCards(dbUser.Id);
    }

    public UserDetail GetUserDetail(string username, string password)
    {
        var dbUser = FindUser(username, password);
        foreach (var userDetail in UserDetails)
        {
            if (userDetail.UserId == dbUser.Id)
            {
                return userDetail;
            }
        }
        return null;
    }

    public void AddCard(AppUser user, Guid cardId)
    {
        var dbUser = FindUser(user.Username, user.Password);
        dbUser.CardsId.Add(cardId);
    }

    public void DeleteCard(AppUser user, Guid cardId)
    {
        var dbUser = FindUser(user.Username, user.Password);
        dbUser.CardsId.Remove(cardId);
    }

    public void AddDetails(AppUser user, Guid userDetailId)
    {
        var dbUser = FindUser(user.Username, user.Password);
        dbUser.UserDetailId = userDetailId;
    }

    public void DeleteDetails(AppUser user)
    {
        var dbUser = FindUser(user.Username, user.Password);
        dbUser.UserDetailId = Guid.Empty;
    }

    public AppUser FindUser(string username, string password)
    {
        for (int i = 0; i < AppUsers.Count; i++)
        {
            if (AppUsers[i].Username == username)
            {
                if (AppUsers[i].Password == password)
                {
                    return AppUsers[i];
                }
            }
        }
        return null;
    }
    public AppUser FindUser(Guid userId)
    {
        for (int i = 0; i < AppUsers.Count; i++)
        {
            if (AppUsers[i].Id == userId)
            {
                return AppUsers[i];
            }
        }
        return null;
    }
    public AppUser FindUser(string username)
    {
        for (int i = 0; i < AppUsers.Count; i++)
        {
            if (AppUsers[i].Username == username)
            {
                return AppUsers[i];
            }
        }
        return null;
    }
    public AppUser FindUser(string email, int value)
    {
        for (int i = 0; i < AppUsers.Count; i++)
        {
            if (AppUsers[i].Email == email)
            {
                return AppUsers[i];
            }
        }
        return null;
    }
    public List<Domain.Card.Entities.BankCard> GetCards(Guid userId)
    {
        List<Domain.Card.Entities.BankCard> userCards = new List<Domain.Card.Entities.BankCard>();
        foreach (var card in BankCards)
        {
            if (card.UserId == userId)
            {
                userCards.Add(card);
            }            
        }
        return userCards;
    }

    public AppUser GetUser(Guid userId)
    {
        for (int i = 0; i < AppUsers.Count; i++)
        {
            if (AppUsers[i].Id == userId)
            {
                return AppUsers[i];
            }
        }
        return null;
    }
}