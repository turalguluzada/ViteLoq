using System.Text.Json;
using Restaurant.Domain.Card.Entities;
using Restaurant.Domain.Tables.Entities;
using Restaurant.Domain.User.Entities;

namespace Restaurant.Infrastructure.Context;

public static class AppDbContext
{
    public static List<AppUser> AppUsers = new List<AppUser>();
    public static List<UserDetail> UserDetails = new List<UserDetail>();
    public static List<BankCard> BankCards = new List<BankCard>();
    public static List<Domain.Restaurant.Entities.Restaurant> Restaurants = new List<Domain.Restaurant.Entities.Restaurant>();

    public static List<Table> Tables = new List<Table>();

    public static string DbDirectory()
    {
        string rootDir = Environment.CurrentDirectory;
        string contentDir = Path.Combine(rootDir, "AppDbContext");
        if (!Directory.Exists(contentDir))
            Directory.CreateDirectory(contentDir);
        return contentDir;
    }

    public static void RefreshUserDb()
    {
        string filePath = Path.Combine(DbDirectory(), "Users.json");
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            string AppUsersJson = JsonSerializer.Serialize(AppUsers);
            sw.Write(AppUsersJson);
        }
    }

    public static void RefreshUserList()
    {
        string filePath = Path.Combine(DbDirectory(), "Users.json");
        if (!File.Exists(filePath))
            File.Create(filePath);
        using (StreamReader sr = new StreamReader(filePath))
        {
            string AppUsersJson = sr.ReadToEnd();
            List<AppUser> dbUsers = JsonSerializer.Deserialize<List<AppUser>>(AppUsersJson);
            if (dbUsers != null)
                AppUsers = dbUsers;
        }
    }
    public static void RefreshBankCardDb()
    {
        string filePath = Path.Combine(DbDirectory(), "BankCards.json");
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            string BankCardsJson = JsonSerializer.Serialize(BankCards);
            sw.Write(BankCardsJson);
        }
    }

    public static void RefreshBankCardList()
    {
        string filePath = Path.Combine(DbDirectory(), "BankCards.json");
        if (!File.Exists(filePath))
            File.Create(filePath);
        using (StreamReader sr = new StreamReader(filePath))
        {
            string BankCardsJson = sr.ReadToEnd();
            List<BankCard> dbBankCards = JsonSerializer.Deserialize<List<BankCard>>(BankCardsJson);
            if (dbBankCards != null)
                BankCards = dbBankCards;
        }
    }
    // public static void RefreshUserDb()
    // {
    //     string filePath = Path.Combine(DbDirectory(), "Users.json");
    //
    //     if (!File.Exists(filePath))
    //         File.WriteAllText(filePath, "[]");
    //
    //     string appUsersJson = File.ReadAllText(filePath);
    //     AppUsers = JsonSerializer.Deserialize<List<AppUser>>(appUsersJson) ?? new List<AppUser>();
    //
    //     if (AppUsers.Count > 0)
    //     {
    //         string serialized = JsonSerializer.Serialize(AppUsers);
    //         File.WriteAllText(filePath, serialized);
    //     }
    // }
}