using Restaurant.Application.Interfaces.BankCard;
using Restaurant.Application.Interfaces.Restaurant;
using Restaurant.Application.Interfaces.User;
using Restaurant.Domain.Card.Entities;
using Restaurant.Domain.User.Entities;
using Restaurant.Shared.Extensions;

namespace Restaurant.Views;

public class Sections
{
    private readonly IUserService _userService;
    private readonly IBankCardService _bankCardService;
    private readonly IRestaurantService _restaurantService;
    
    public Sections(IUserService userService, IBankCardService bankCardService, IRestaurantService restaurantService)
    {
        _userService = userService;
        _bankCardService = bankCardService;
        _restaurantService = restaurantService;
    }
    
    public void ShowUserInformation(AppUser appUser)
    {
        Console.Clear();
        Console.WriteLine($"Id: {appUser.Id}" +
                          $"\nUsername: {appUser.Username}" +
                          $"\nPassword: {appUser.Password}" +
                          $"\nMail: {appUser.Email}" +
                          $"\nRole: {appUser.Role}");
    }

    public void ShowUserBankCards(AppUser appUser)
    {
        _userService.ShowUserCards(appUser);
    }

    public void IncreaseBalance(AppUser appUser)
    {
        // string cardNumber = Helpers.GiveConditionalNumber("bank card number for increasing balance", 16, "0000 - 0000 - 0000 - 0000");
        List<BankCard> bankCards = _userService.GetUserCards(appUser);
        List<string> bankCardNumbers = new List<string>();
        Console.WriteLine("Your Bank Cards:");
        for (int i = 0; i < bankCards.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Helpers.NormalizedBankCardNumber(bankCards[i].CardNumber)}");
            bankCardNumbers.Add(bankCards[i].CardNumber);
        }

        int choice = Helpers.GiveNumber("choice for bank card number for increasing balance");
        if (bankCardNumbers[choice - 1] != null)
        {
            int amount = Helpers.GiveNumber("amount for bank card number for increasing balance");
            if (_bankCardService.IncreaseBalance(bankCardNumbers[choice - 1], amount))
            {
                var card = _bankCardService.GetBankCard(bankCardNumbers[choice - 1]);
                Console.Clear();
                Console.WriteLine($"Your Bank card balance has been increased.\nYour Balance is {card.Balance}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Your Bank card balance has not been increased.");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Your Bank Card number is invalid.");
        }
    }
    public void TransferMoney(AppUser appUser)
    {
        List<BankCard> bankCards = _userService.GetUserCards(appUser);
        List<string> bankCardNumbers = new List<string>();
        Console.WriteLine("Your Bank Cards:");
        for (int i = 0; i < bankCards.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Helpers.NormalizedBankCardNumber(bankCards[i].CardNumber)}");
            bankCardNumbers.Add(bankCards[i].CardNumber);
        }

        int choice = Helpers.GiveNumber("choice for bank card number for increasing balance");
        if (bankCardNumbers[choice - 1] != null)
        {
            string receiverCardNumber = Helpers.GiveConditionalNumber("receiver bank card number", 16, "0000 - 0000 - 0000 - 0000");
            Console.Clear();
            int amount = Helpers.GiveNumber("amount for bank card number for send");
            var card = _bankCardService.GetBankCard(bankCardNumbers[choice - 1]);

            _bankCardService.TransferMoney(bankCardNumbers[choice - 1], receiverCardNumber, amount);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Your Bank Card number is invalid.");
        }
    }
    public void BuildRestaurant(AppUser appUser)
    {
        List<BankCard> bankCards = _userService.GetUserCards(appUser);
        List<string> bankCardNumbers = new List<string>();
        Console.WriteLine("Your Bank Cards:");
        for (int i = 0; i < bankCards.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Helpers.NormalizedBankCardNumber(bankCards[i].CardNumber)}");
            bankCardNumbers.Add(bankCards[i].CardNumber);
        }

        int choice = Helpers.GiveNumber("choice for bank card number for increasing balance");
        if (bankCardNumbers[choice - 1] != null)
        {
            Console.Clear();
            int amount = Helpers.GiveNumber("amount for restaurant cost");
            if (_bankCardService.DecreaseBalance(bankCardNumbers[choice - 1], amount))
            {
                _restaurantService.CreateRestaurant(appUser.Id, amount);
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Your Bank Card number is invalid.");
        }
    }

    public void BuyThisRestaurant(AppUser appUser, Domain.Restaurant.Entities.Restaurant restaurant)
    {
        List<BankCard> bankCards = _userService.GetUserCards(appUser);
        List<string> bankCardNumbers = new List<string>();
        Console.WriteLine("Your Bank Cards:");
        for (int i = 0; i < bankCards.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Helpers.NormalizedBankCardNumber(bankCards[i].CardNumber)}");
            bankCardNumbers.Add(bankCards[i].CardNumber);
        }

        int choice = Helpers.GiveNumber("choice for bank card number for increasing balance");
        if (bankCardNumbers[choice - 1] != null)
        {
            Console.Clear();
            var oldUser = _userService.GetUser(restaurant.UserId);
            List<BankCard> oldUserCards = _userService.GetUserCards(oldUser);
            if (_bankCardService.DecreaseBalance(bankCardNumbers[choice - 1], restaurant.PropertyValue))
            {
                _restaurantService.BuyRestaurant(restaurant.Name, appUser.Id);
                _bankCardService.IncreaseBalance(oldUserCards[0].CardNumber, restaurant.PropertyValue);
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Your Bank Card number is invalid.");
        }
    }

    public void CreateTable(Domain.Restaurant.Entities.Restaurant restaurant, AppUser appUser)
    {
        List<BankCard> bankCards = _userService.GetUserCards(appUser);
        List<string> bankCardNumbers = new List<string>();
        Console.WriteLine("Your Bank Cards:");
        for (int i = 0; i < bankCards.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Helpers.NormalizedBankCardNumber(bankCards[i].CardNumber)}");
            bankCardNumbers.Add(bankCards[i].CardNumber);
        }

        int choice = Helpers.GiveNumber("choice for bank card number for increasing balance");
        if (bankCardNumbers[choice - 1] != null)
        {
            Console.Clear();
            int amount = _restaurantService.CreateTable(restaurant);
            _bankCardService.DecreaseBalance(bankCardNumbers[choice - 1], amount);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Your Bank Card number is invalid.");
        }
    }
}