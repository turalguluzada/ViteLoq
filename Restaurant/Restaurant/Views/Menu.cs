using Restaurant.Application.Interfaces.User;
using Restaurant.Application.Services.User;
using Restaurant.Domain.User.Entities;
using Restaurant.Domain.User.Interfaces;
using Restaurant.Infrastructure.Repositories.User;
using Restaurant.Shared.Extensions;

namespace Restaurant.Views;

public class Menu
{
    private readonly IUserService _userService;
    private readonly Panels _panels;
    public Menu(IUserService userService, Panels panels)
    {
        _userService = userService;
        _panels = panels;
    }
    public void Run()
    {
        run:
        AppUser appUser = new AppUser();
        
        if (appUser.Id == Guid.Empty)
            appUser = AnonimUser();
        
        SignedUser(appUser);
        goto run;
    }
    
    
    
    
    #region MenuSides    
    public AppUser AnonimUser()
    {
        AppUser appUser = new AppUser();
        tryAgain:
        Helpers.Loading(3);
        Console.WriteLine("Welcome to the restaurant system !" +
                          "\n1. Login" +
                          "\n2. Register");
        int choice = Helpers.GiveNumber("choice");
        
        switch (choice)
        {
            case 1:
                Helpers.Loading(2);
                Console.Clear();
                appUser = _userService.GetUser();
                if (appUser == null)
                {
                    Console.Clear();
                    Console.WriteLine("Username or password is incorrect");
                    goto tryAgain;
                }
                return appUser;
            case 2:
                Helpers.Loading(2);
                _userService.AddUser();
                Console.Clear();
                Console.WriteLine("Successfully added user");
                goto tryAgain;
        }
        return appUser;
    }
    public void SignedUser(AppUser appUser)
    {
        Helpers.Loading(3);
        _panels.UserPanel(appUser);
    }
    #endregion

    
    
    
    
    public bool Again()
    {
        Console.WriteLine("Press any key to go back.");
        Console.ReadLine();
        return true;
    }
    
    
}