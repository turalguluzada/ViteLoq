using Restaurant.Domain.User.Entities;

namespace Restaurant.Application.Interfaces.Restaurant;

public interface IRestaurantService
{
    public void CreateRestaurant(Guid userId, int cost);
    public Domain.Restaurant.Entities.Restaurant GetRestaurant(string restaurantName);
    public void GetRestaurantInfo(Domain.Restaurant.Entities.Restaurant restaurant);
    public int CreateTable(Domain.Restaurant.Entities.Restaurant restaurant);
    public bool BuyRestaurant(string restaurantName, Guid userId);
    public int ReserveTable(Domain.Restaurant.Entities.Restaurant restaurant, AppUser appUser);
    public void UnReserveTable(Domain.Restaurant.Entities.Restaurant restaurant, AppUser appUser);
    public List<string> GetRestaurantNames();
    public void ShowAllTablesInfo(Domain.Restaurant.Entities.Restaurant restaurant);
    public void ShowUnReservedTablesInfo(Domain.Restaurant.Entities.Restaurant restaurant);
}