using Restaurant.Domain.Restaurant.Interfaces;
using Restaurant.Domain.Tables.Entities;

namespace Restaurant.Infrastructure.Repositories.Restaurant;

public class RestaurantRepository : IRestaurantRepository
{
    public List<Domain.Restaurant.Entities.Restaurant> Restaurants = Context.AppDbContext.Restaurants;
    public List<Table> Tables = Context.AppDbContext.Tables;
    
    public void CreateRestaurant(Domain.Restaurant.Entities.Restaurant restaurant)
    {
        Restaurants.Add(restaurant);
    }

    public Domain.Restaurant.Entities.Restaurant GetRestaurant(string restaurantName)
    {
        for (int i = 0; i < Restaurants.Count; i++)
        {
            if (Restaurants[i].Name == restaurantName)
            {
                return Restaurants[i];
            }
        }
        return null;
    }
    
    public void CreateTable(Table table)
    {
        Tables.Add(table);
    }


    public Table GetTable(string tableName)
    {
        for (int i = 0; i < Tables.Count; i++)
        {
            if (Tables[i].Name == tableName)
            {
                return Tables[i];
            }
        }
        return null;
    }
    public Table GetTable(Guid tableId)
    {
        for (int i = 0; i < Tables.Count; i++)
        {
            if (Tables[i].Id == tableId)
            {
                return Tables[i];
            }
        }
        return null;
    }

    public void ReserveTable(Table table, Guid userId)
    {
        table.IsReserved = true;
        table.ReservedUserId = userId;
    }

    public void UnReserveTable(Table table, Guid userId)
    {
        table.IsReserved = false;
        table.ReservedUserId = Guid.Empty;
    }

    public List<string> GetRestaurantNames()
    {
        List<string> restaurantNames = new List<string>();
        for (int i = 0; i < Restaurants.Count; i++)
        {
            restaurantNames.Add(Restaurants[i].Name);
        }
        return restaurantNames;
    }

    public List<Table> GetUnReservedTables(Domain.Restaurant.Entities.Restaurant restaurant)
    {
        var tables = Tables.Where(x => x.IsReserved == false && x.RestaurantId == restaurant.Id).ToList();
        return tables;
    }
    public List<Table> GetTables(Domain.Restaurant.Entities.Restaurant restaurant)
    {
        var tables = Tables.Where(x => x.RestaurantId == restaurant.Id).ToList();
        return tables;
    }
}