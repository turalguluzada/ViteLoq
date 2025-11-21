using Restaurant.Application.Interfaces.Restaurant;
using Restaurant.Domain.Restaurant.Interfaces;
using Restaurant.Domain.Tables.Entities;
using Restaurant.Domain.User.Entities;
using Restaurant.Infrastructure.Repositories.Restaurant;
using Restaurant.Shared.Extensions;

namespace Restaurant.Application.Services.Restaurant;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }
    public void CreateRestaurant(Guid userId, int cost)
    {
        Domain.Restaurant.Entities.Restaurant restaurant = new Domain.Restaurant.Entities.Restaurant();
        restaurant.Id = Guid.NewGuid();
        restaurant.Name = Helpers.GiveString("Restaurant name");
        restaurant.PropertyValue = cost;
        restaurant.UserId = userId;
        
        _restaurantRepository.CreateRestaurant(restaurant);
    }

    public Domain.Restaurant.Entities.Restaurant GetRestaurant(string restaurantName)
    {
        return _restaurantRepository.GetRestaurant(restaurantName);
    }

    public void GetRestaurantInfo(Domain.Restaurant.Entities.Restaurant restaurant)
    {
        Console.WriteLine($"Restaurant ID: {restaurant.Id}" +
                          $"\nRestaurant Name: {restaurant.Name}" +
                          $"\nRestaurant PropertyValue: {restaurant.PropertyValue}" +
                          $"\nRestaurant UserID: {restaurant.UserId}");
        foreach (var tableId in restaurant.TableIds)
        {
            Table table = _restaurantRepository.GetTable(tableId);
            Console.WriteLine("");
            Console.WriteLine("#######");
            Console.WriteLine($"Table ID: {table.Id}" +
                              $"\nTable Name: {table.Name}" +
                              $"\nTable RestaurantId: {table.RestaurantId}" +
                              $"\nTable ReservedUserId: {table.ReservedUserId}" +
                              $"\nTable Is Reserved: {table.IsReserved}" +
                              $"\nTable Cost: {table.Cost}" );
        }
    }
    
    public int CreateTable(Domain.Restaurant.Entities.Restaurant restaurant)
    {
        Table table = new Table();
        table.Id = Guid.NewGuid();
        table.Name = Helpers.GiveString("table name");
        table.RestaurantId = restaurant.Id;
        table.ReservedUserId = Guid.NewGuid();
        table.IsReserved = false;
        table.Cost = Helpers.GiveNumber("table cost");
        _restaurantRepository.CreateTable(table);
        return table.Cost;
    }

    public bool BuyRestaurant(string restaurantName, Guid userId)
    {
        var restaurant = _restaurantRepository.GetRestaurant(restaurantName);
        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found");
            return false;
        }
        restaurant.UserId = userId;
        return true;
    }

    public int ReserveTable(Domain.Restaurant.Entities.Restaurant restaurant, AppUser appUser)
    {
        string tableName = Helpers.GiveString("table name");
        var table = _restaurantRepository.GetTable(tableName);
        if (table == null)
            Console.WriteLine("Table not found");
        Guid userId = appUser.Id;
        _restaurantRepository.ReserveTable(table, userId);
        restaurant.PropertyValue += table.Cost;
        return table.Cost;
    }
    public void UnReserveTable(Domain.Restaurant.Entities.Restaurant restaurant, AppUser appUser)
    {
        string tableName = Helpers.GiveString("table name");
        var table = _restaurantRepository.GetTable(tableName);
        if (table == null)
            Console.WriteLine("Table not found");
        Guid userId = appUser.Id;
        _restaurantRepository.UnReserveTable(table, userId);
    }

    public void ShowAllTablesInfo(Domain.Restaurant.Entities.Restaurant restaurant)
    {
        var tables = _restaurantRepository.GetTables(restaurant);
        if (tables.Count > 0)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                InfoTable(tables[i]);
            }
        }
    }
    public void ShowUnReservedTablesInfo(Domain.Restaurant.Entities.Restaurant restaurant)
    {
        var tables = _restaurantRepository.GetUnReservedTables(restaurant);
        if (tables.Count > 0)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                InfoTable(tables[i]);
            }
        }
    }

    public void InfoTable(Table table)
    {
        Console.WriteLine("#######################");
        Console.WriteLine($"Table Id: {table.Id}" +
                          $"\nTable Name: {table.Name}" +
                          $"\nTable RestaurantId: {table.RestaurantId}" +
                          $"\nTable ReservedUserId: {table.ReservedUserId}" +
                          $"\nTable Is Reserved: {table.IsReserved}" +
                          $"\nTable Cost: {table.Cost}\n");
        Console.WriteLine("#######################");
    }
    public List<string> GetRestaurantNames()
    {
        return _restaurantRepository.GetRestaurantNames();
    }

}