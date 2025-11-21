using Restaurant.Application.Interfaces.User;
using Restaurant.Domain.Card.Entities;
using Restaurant.Domain.User.Entities;
using Restaurant.Domain.User.Interfaces;
using Restaurant.Infrastructure.Repositories.User;
using Restaurant.Shared.Extensions;

namespace Restaurant.Application.Services.User;

public class UserService : IUserService
{  
    // public IUserRepository _userRepository = new UserRepository();
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser()
    {
        Console.Clear();
        AppUser appUser = new AppUser();
        
        tryUsername:
        appUser.Username = Helpers.GiveString("username");
        if (UnicUsername(appUser.Username))
        {
            Console.WriteLine("Username already exists!");
            goto tryUsername;
        }
        
        tryEmail:
        appUser.Email = Helpers.GiveEmail();
        // appUser.Email = Helpers.GiveString("email");
        if (UnicEmail(appUser.Email))
        {
            Console.WriteLine("Email is already in use");
            goto tryEmail;
        }
        
        appUser.Password = Helpers.GivePassword();
        // appUser.Password = Helpers.GiveString("password");
        
        // appUser.Role = Helpers.GiveString("role");
        appUser.Role = "user";
        appUser.IsActive = true;
        appUser.UserDetailId = Guid.Empty;
        
        _userRepository.AddUser(appUser);
    }

    public void DeleteUser(AppUser appUser)
    {
        _userRepository.DeleteUser(appUser.Id);
    }

    public void UpdateUser(AppUser appUser)
    {
        _userRepository.UpdateUser(appUser);
    }

    public AppUser GetUser()
    {
        string username = Helpers.GiveString("username");
        string password = Helpers.GiveString("password");
        AppUser appUser = _userRepository.GetUser(username, password);
        return appUser;
    }

    public void ShowUserCards(AppUser appUser)
    {
        var cards = GetUserCards(appUser);
        foreach (var card in cards)
        {
            Console.WriteLine("####################################");
            Console.WriteLine($"BankCard ID: {card.Id}" +
                              $"\nCard Number: {Helpers.NormalizedBankCardNumber(card.CardNumber)}" +
                              $"\nCard expired date: {card.ExpirationDate}" +
                              $"\nCard cvv: {card.Cvv}" +
                              $"\nCard pin: {card.Pin}" +
                              $"\nCard balance: {card.Balance}");
        }
    }

    public List<Domain.Card.Entities.BankCard> GetUserCards(AppUser appUser)
    {
        List<Domain.Card.Entities.BankCard> cards = _userRepository.ShowUserCards(appUser.Username);
        return cards;
    }

    public void GetUserDetail(AppUser appUser)
    {
        _userRepository.GetUserDetail(appUser.Username, appUser.Password);
    }

    public void GetCards(AppUser appUser)
    {
        _userRepository.GetCards(appUser.Id);
    }

    public void AddCard(AppUser appUser, Domain.Card.Entities.BankCard bankCard)
    {
        _userRepository.AddCard(appUser, bankCard.Id);
    }

    public void DeleteCard(AppUser appUser, Domain.Card.Entities.BankCard bankCard)
    {
        _userRepository.DeleteCard(appUser, bankCard.Id);
    }

    public AppUser GetUser(Guid userId)
    {
        return _userRepository.GetUser(userId);
    }

    public void AddDetails(AppUser appUser, UserDetail userDetail)
    {
        _userRepository.AddDetails(appUser, userDetail.Id);
    }

    public void DeleteDetails(AppUser appUser)
    {
        _userRepository.DeleteDetails(appUser);
    }

    public bool UnicUsername(string username)
    {
        AppUser appuser = _userRepository.FindUser(username);
        if (appuser == null)
            return false;
        
        return true;
    }

    public bool UnicEmail(string email)
    {
        AppUser appuser = _userRepository.FindUser(email, 0);
        if (appuser == null)
            return false;
        
        return true;
    }

}