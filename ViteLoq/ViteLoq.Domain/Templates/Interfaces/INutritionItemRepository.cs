using ViteLoq.Domain.Templates.Entities;

namespace ViteLoq.Domain.Templates.Interfaces;

public interface INutritionItemRepository
{
    public Task CreateAsync(NutritionItem nutritionItem);
    public Task UpdateAsync(NutritionItem nutritionItem);
    public Task<NutritionItem> GetByIdAsync(Guid foodId);
    public Task<List<NutritionItem>> GetAllAsync();
    public Task<List<NutritionItem>> SearchByNameAsync(string name);
}