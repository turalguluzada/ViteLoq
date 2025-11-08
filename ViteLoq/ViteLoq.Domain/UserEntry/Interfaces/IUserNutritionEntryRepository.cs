using ViteLoq.Domain.Entities;
using ViteLoq.Domain.UserEntry.Entities;

namespace ViteLoq.Domain.UserEntry.Interfaces;

public interface IUserNutritionEntryRepository
{
    public void Add(UserNutritionEntry entry);
    public void Update(UserNutritionEntry entry);
    public UserNutritionEntry GetByNutritionEntryId(Guid userId, DateTime from, DateTime to);
}