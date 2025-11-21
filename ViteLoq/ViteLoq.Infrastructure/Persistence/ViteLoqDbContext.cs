using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ViteLoq.Domain.Entities;
using ViteLoq.Domain.Goals.Entities;
using ViteLoq.Domain.Interfaces;
using ViteLoq.Domain.Templates.Entities;
using ViteLoq.Domain.UserEntry.Entities;
using ViteLoq.Domain.UserTotal.Entities;

namespace ViteLoq.Infrastructure.Persistence
{
    // public class ViteLoqDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>,
//     ViteLoq.Domain.Interfaces.IUnitOfWork

    public class ViteLoqDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
    {
        public ViteLoqDbContext(DbContextOptions<ViteLoqDbContext> options) : base(options){}
        
        public DbSet<UserGymTarget> UserGymTargets { get; set; }
        public DbSet<UserNutritionTarget> UserNutritionTargets { get; set; }
        
        public DbSet<NutritionItem> NutritionItems { get; set; }
        public DbSet<WorkoutTemplate> WorkoutTemplates { get; set; }
        
        public DbSet<UserNutritionEntry> UserNutritionEntries { get; set; }
        public DbSet<UserWorkoutEntry> UserWorkoutEntries { get; set; }
        
        public DbSet<UserDetail> UserDetails { get; set; }
        
        public DbSet<UserTotalHealth> UserTotalHealth { get; set; }
        public DbSet<UserTotalMental> UserTotalMental { get; set; }
        public DbSet<UserTotalMuscle> UserTotalMuscle { get; set; }
        public DbSet<UserTotalSkin> UserTotalSkin { get; set; }
        
        
        
        // IUnitOfWork implementasiyası:
        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            // burada istəsən əvvəl auditing, domain-events dispatch və s. edə bilərsən
            return base.SaveChangesAsync(ct);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ViteLoqDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            // sənin mapping'lər...
        }
    }
}

