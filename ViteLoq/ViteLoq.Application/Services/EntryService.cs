using ViteLoq.Domain.Interfaces;
using ViteLoq.Domain.Templates.Interfaces;
using ViteLoq.Domain.UserEntry.Interfaces;

namespace ViteLoq.Application.Services
{
    public class EntryService
    {
        // private readonly IUserNutritionEntryRepository _entryRepo;
        // private readonly INutritionItemRepository _nutritionItemRepo;
        // private readonly IUnitOfWork _uow;
        //
        // public EntryService(IUserNutritionEntryRepository entryRepo, INutritionItemRepository nutritionItemRepo, IUnitOfWork uow)
        // {
        //     _entryRepo = entryRepo;
        //     _nutritionItemRepo = nutritionItemRepo;
        //     _uow = uow;
        // }

        // public async Task<Guid> CreateFoodEntryAsync(CreateFoodEntryDto dto, CancellationToken ct = default)
        // {
        //     if (dto.UserId == Guid.Empty) throw new ArgumentException("UserId required");
        //
        //     double calories = 0;
        //     if (dto.FoodId.HasValue)
        //     {
        //         var food = await _nutritionItemRepo.GetByIdAsync(dto.FoodId.Value, ct);
        //         if (food != null)
        //         {
        //             // simple placeholder calculation: per 100g
        //             calories = Math.Round((food.KcalPer100g / 100.0) * dto.QuantityValue, 2);
        //         }
        //     }
        //
        //     var entry = new UserEntry
        //     {
        //         Id = Guid.NewGuid(),
        //         UserId = dto.UserId,
        //         FoodId = dto.FoodId,
        //         DateTime = dto.DateTime,
        //         QuantityValue = dto.QuantityValue,
        //         QuantityUnit = dto.QuantityUnit,
        //         Calories = calories,
        //         MealType = dto.MealType,
        //         Category = dto.Category,
        //         SubCategory = dto.SubCategory,
        //         Notes = dto.Notes
        //     };
        //
        //     await _entryRepo.AddFoodEntryAsync(entry, ct);
        //     await _uow.SaveChangesAsync(ct); // commit
        //
        //     // Optionally: publish domain event UserFoodEntryCreated (out-of-scope here)
        //     return entry.Id;
        // }
        //
        // public Task<IEnumerable<UserEntry>> GetFoodEntriesForUserAsync(Guid userId, DateTime from, DateTime to, CancellationToken ct = default)
        // {
        //     return _entryRepo.GetFoodEntriesForUserAsync(userId, from, to, ct);
        // }
    }
}