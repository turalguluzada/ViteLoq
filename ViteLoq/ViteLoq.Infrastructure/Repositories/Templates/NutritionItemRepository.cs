using Microsoft.EntityFrameworkCore;
using ViteLoq.Domain.Templates.Entities;
using ViteLoq.Domain.Templates.Interfaces;
using ViteLoq.Infrastructure.Persistence;

namespace ViteLoq.Infrastructure.Repositories.Templates;

public class NutritionItemRepository : INutritionItemRepository
{
    private readonly ViteLoqDbContext _context;

    public NutritionItemRepository(ViteLoqDbContext context)
    {
        _context = context;
    }
    public async Task<NutritionItem> GetByIdAsync(Guid id)
    {
        var dbNutrition = await _context.NutritionItems.Where(n => n.Id == id).FirstOrDefaultAsync();
        return dbNutrition;
    }

    public async Task<List<NutritionItem>> GetAllAsync()
    {
        var dbNutritions = await _context.NutritionItems.ToListAsync();
        return dbNutritions;
    }
    public async Task<List<NutritionItem>> SearchByNameAsync(string name)
    {
        var dbNutritions = await _context.NutritionItems.Where(n => n.Name == name).ToListAsync();
        return dbNutritions;
    }

    public async Task CreateAsync(NutritionItem item)
    {
        await _context.NutritionItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(NutritionItem item)
    {
        _context.NutritionItems.Update(item);
        await _context.SaveChangesAsync();
    }
}