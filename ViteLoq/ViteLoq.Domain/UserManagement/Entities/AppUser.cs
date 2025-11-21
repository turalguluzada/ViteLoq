using Microsoft.AspNetCore.Identity;
using ViteLoq.Domain.Base.Entities;
using ViteLoq.Domain.UserEntry.Entities;
using ViteLoq.Domain.UserTotal.Entities;

namespace ViteLoq.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
    
    // public UserProfile UserProfile { get; set; }
    // public UserTargets UserTargets { get; set; }
    // public UserDailyProgress UserDailyProgress { get; set; }
    
    public List<UserNutritionEntry> UserNutritionEntry { get; set; }
    public List<UserWorkoutEntry> UserWorkoutEntry { get; set; }
    
    // public UserTotalHealth UserTotalHealth { get; set; }
    // public UserTotalMental UserTotalMental { get; set; }
    // public UserTotalMuscle UserTotalMuscle { get; set; }
    // public UserTotalSkin UserTotalSkin { get; set; }
}