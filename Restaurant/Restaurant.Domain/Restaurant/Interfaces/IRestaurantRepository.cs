using Restaurant.Domain.Tables.Entities;

namespace Restaurant.Domain.Restaurant.Interfaces;

public interface IRestaurantRepository
{
    public void CreateRestaurant(Domain.Restaurant.Entities.Restaurant restaurant);
    public Domain.Restaurant.Entities.Restaurant GetRestaurant(string restaurantName);
    public void CreateTable(Table table);
    public Table GetTable(string tableName);
    public Table GetTable(Guid tableId);
    public void ReserveTable(Table table, Guid userId);
    public void UnReserveTable(Table table, Guid userId);
    public List<string> GetRestaurantNames();
    public List<Table> GetTables(Domain.Restaurant.Entities.Restaurant restaurant);
    public List<Table> GetUnReservedTables(Domain.Restaurant.Entities.Restaurant restaurant);
}