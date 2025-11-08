using ViteLoq.Application.DTOs.Templates.Nutrition;
using ViteLoq.Domain.Interfaces;
using ViteLoq.Domain.Templates.Entities;
using ViteLoq.Domain.Templates.Interfaces;

namespace ViteLoq.Application.Interfaces.Templates;

public interface INutritionItemService
{
    public Task<Guid> CreateAsync(CreateNutritionItemDto nutritionItemDto,
        CancellationToken cancellationToken = default);

    public Task<NutritionItem> GetByIdAsync(Guid id);
    
    public Task<List<NutritionItem>> GetAllAsync();
}