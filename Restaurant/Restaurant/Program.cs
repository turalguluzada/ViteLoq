using System;
using Restaurant.Application.Interfaces.BankCard;
using Restaurant.Application.Interfaces.Restaurant;
using Restaurant.Application.Interfaces.User;
using Restaurant.Application.Services.BankCard;
using Restaurant.Application.Services.Restaurant;
using Restaurant.Application.Services.User;
using Restaurant.Domain.Card.Interfaces;
using Restaurant.Domain.Restaurant.Interfaces;
using Restaurant.Domain.User.Interfaces;
using Restaurant.Infrastructure.Repositories.BankCard;
using Restaurant.Infrastructure.Repositories.Restaurant;
using Restaurant.Views;
using Restaurant.Infrastructure.Repositories.User;

IUserRepository userRepository = new UserRepository();
IUserService userService = new UserService(userRepository);
IBankCardRepository bankCardRepository = new BankCardRepository();
IBankCardService bankCardService = new BankCardService(bankCardRepository);
IRestaurantRepository restaurantRepository = new RestaurantRepository();
IRestaurantService restaurantService = new RestaurantService(restaurantRepository);

Sections sections = new Sections(userService, bankCardService, restaurantService); 
Panels panels = new Panels(userService,bankCardService, restaurantService, sections);
Menu menu = new Menu(userService, panels);

            
menu.Run();
 