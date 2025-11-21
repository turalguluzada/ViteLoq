using Restaurant.Application.Interfaces.BankCard;
using Restaurant.Application.Interfaces.Restaurant;
using Restaurant.Application.Interfaces.User;
using Restaurant.Domain.User.Entities;
using Restaurant.Shared.Extensions;

namespace Restaurant.Views;

public class Panels
{
    private readonly IUserService _userService;
    private readonly IBankCardService _bankCardService;
    private readonly IRestaurantService _restaurantService;
    private readonly Sections _sections;
    
    public Panels(IUserService userService, IBankCardService bankCardService, IRestaurantService restaurantService, Sections sections)
    {
        _userService = userService;
        _bankCardService = bankCardService;
        _restaurantService = restaurantService;
        _sections = sections;
    }
    
    public void UserPanel(AppUser appUser)
    {
        Helpers.Loading(2);
        tryAgain:
        Console.WriteLine($"Welcome to the restaurant {appUser.Username}" +
                          $"\n1. Show User Information" +
                          $"\n2. Show User Bank Cards" +
                          $"\n3. Add Card" +
                          $"\n4. Increase Balance" +
                          $"\n5. Transfer money another bank card" +
                          $"\n6. Show All Restaurants" +
                          $"\n7. Build Restaurant" +
                          $"\n0. Exit");
        int choice = Helpers.GiveNumber("choice");
        switch (choice)
        {
            case 1:
                Helpers.Loading(2);
                _sections.ShowUserInformation(appUser);
                if (Again())
                    goto tryAgain;
                break;
            case 2:
                Helpers.Loading(2);
                _sections.ShowUserBankCards(appUser);
                if (Again())
                    Helpers.Loading(2);
                    goto tryAgain;
                break;
            case 3:
                Helpers.Loading(2);
                _bankCardService.CreateBankCard(appUser);
                Helpers.Loading(2);
                goto tryAgain;
            case 4:
                Helpers.Loading(2);
                _sections.IncreaseBalance(appUser);
                goto tryAgain;
            case 5:
                Helpers.Loading(2);
                _sections.TransferMoney(appUser);
                goto tryAgain;
            case 6:
                Helpers.Loading(2);
                RestaurantPanel(appUser);
                goto tryAgain;
            case 7:
                Helpers.Loading(2);
                _sections.BuildRestaurant(appUser);
                goto tryAgain;
            default:
                break;
        }
    }

    public void RestaurantPanel(AppUser appUser)
    {
        var restaurantNames = _restaurantService.GetRestaurantNames();
        if (restaurantNames.Count < 1)
            return;
        
        Console.WriteLine("Welcome to the restaurant you are");
        for (int i = 0; i < restaurantNames.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {restaurantNames[i]}");
        }
        int choice = Helpers.GiveNumber("choice");
        var restaurant = _restaurantService.GetRestaurant(restaurantNames[choice - 1]);
        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found");
            return;
        }

        if (restaurant.UserId == appUser.Id)
        {
            RestaurantAdminPanel(appUser, restaurant);
        }
        else
        {
            RestaurantUserPanel(appUser, restaurant);
        }
            
        
    }
    public void RestaurantAdminPanel(AppUser appUser, Domain.Restaurant.Entities.Restaurant restaurant)
    {
        Helpers.Loading(3);
        tryAdmin:
        Console.WriteLine($"Welcome to your restaurant boss" +
                          $"\n 1. Show restaurant information" +
                          $"\n 2. Create table" +
                          $"\n 3. Unreserve table" +
                          $"\n 4. Show all tables" +
                          $"\n 5. Reserve table" +
                          $"\n 0. Exit");
        int choice = Helpers.GiveNumber("choice");
        switch (choice)
        {
            case 1:
                Helpers.Loading(2);
                _restaurantService.GetRestaurantInfo(restaurant);
                goto tryAdmin;
            case 2:
                Helpers.Loading(2);
                _sections.CreateTable(restaurant, appUser);
                goto tryAdmin;
            case 3:
                Helpers.Loading(2);
                _restaurantService.UnReserveTable(restaurant, appUser);
                goto tryAdmin;
            case 4:
                Helpers.Loading(2);
                _restaurantService.ShowAllTablesInfo(restaurant);
                goto tryAdmin;
            case 5:
                Helpers.Loading(2);
                _restaurantService.ReserveTable(restaurant, appUser);
                goto tryAdmin;
            default:
                break;
        }
        
    }
    public void RestaurantUserPanel(AppUser appUser, Domain.Restaurant.Entities.Restaurant restaurant)
    {
        Helpers.Loading(2);
        tryUser:
        Console.WriteLine($"Welcome to the restaurant you are" +
                          $"\n 1. Show restaurant information" +
                          $"\n 2. Reserve table" +
                          $"\n 3. Buy this restaurant" +
                          $"\n 4. Show unreserved tables" +
                          $"\n 0. Exit");
        int choice = Helpers.GiveNumber("choice");
        switch (choice)
        {
            case 1:
                Helpers.Loading(2);
                _restaurantService.GetRestaurantInfo(restaurant);
                goto tryUser;
            case 2:
                Helpers.Loading(2);
                _restaurantService.ReserveTable(restaurant, appUser);
                goto tryUser;
            case 3:
                Helpers.Loading(2);
                _sections.BuyThisRestaurant(appUser, restaurant);
                goto tryUser;
            case 4:
                Helpers.Loading(2);
                _restaurantService.ShowUnReservedTablesInfo(restaurant);
                goto tryUser;
            case 5:
                Helpers.Loading(2);
                _restaurantService.ShowAllTablesInfo(restaurant);
                goto tryUser;
            default:
                break;
        }
    }
    
    
    
    
    public bool Again()
    {
        Console.WriteLine("\nPress any key to go back.");
        Console.ReadLine();
        Console.Clear();
        return true;
    }
}