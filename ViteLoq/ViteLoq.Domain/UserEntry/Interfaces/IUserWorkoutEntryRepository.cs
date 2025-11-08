using ViteLoq.Domain.UserEntry.Entities;

namespace ViteLoq.Domain.UserEntry.Interfaces;

public interface IUserWorkoutEntryRepository
{
    public void Add(UserWorkoutEntry entry);
    public void Update(UserWorkoutEntry entry);
    public UserWorkoutEntry GetByWorkoutEntryId(Guid userId, DateTime from, DateTime to);
}