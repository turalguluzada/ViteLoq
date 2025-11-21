using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViteLoq.Domain.Entities;
using ViteLoq.Domain.UserManagement.Interfaces;
using ViteLoq.Infrastructure.Persistence;

namespace ViteLoq.Infrastructure.Repositories.UserManagement
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ViteLoqDbContext _ctx;

        public UserRepository(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ViteLoqDbContext ctx)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        // ---------------- Identity / lifecycle ----------------
        public async Task<IdentityResult> CreateAsync(AppUser user, string password, IEnumerable<string>? roles = null, CancellationToken ct = default)
        {
            var res = await _userManager.CreateAsync(user, password);
            if (!res.Succeeded) return res;

            if (roles != null && roles.Any())
            {
                // ensure roles exist (optional)
                foreach (var r in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(r))
                        await _roleManager.CreateAsync(new IdentityRole<Guid>(r));
                }

                var roleRes = await _userManager.AddToRolesAsync(user, roles);
                if (!roleRes.Succeeded) return roleRes;
            }

            return IdentityResult.Success;
        }

        public Task<AppUser?> GetByIdAsync(Guid userId, CancellationToken ct = default)
        {
            return _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId, ct);
        }

        public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken ct = default)
        {
            return _userManager.UpdateAsync(user);
        }

        // ---------------- Email / tokens ----------------
        public Task<string> GenerateEmailConfirmationTokenAsync(AppUser user, CancellationToken ct = default)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token, CancellationToken ct = default)
        {
            return _userManager.ConfirmEmailAsync(user, token);
        }

        // ---------------- Authentication helpers ----------------
        public Task<bool> CheckPasswordAsync(AppUser user, string password, CancellationToken ct = default)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword, CancellationToken ct = default)
        {
            return _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        // ---------------- Roles & Claims ----------------
        public Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken ct = default)
        {
            return _userManager.GetRolesAsync(user);
        }

        public Task AddToRoleAsync(AppUser user, string role, CancellationToken ct = default)
        {
            return _userManager.AddToRoleAsync(user, role);
        }

        public Task<bool> IsInRoleAsync(AppUser user, string role, CancellationToken ct = default)
        {
            return _userManager.IsInRoleAsync(user, role);
        }

        public Task<IList<Claim>> GetClaimsAsync(AppUser user, CancellationToken ct = default)
        {
            return _userManager.GetClaimsAsync(user);
        }

        // ---------------- UserDetail (profile) ----------------
        public async Task<UserDetail?> GetUserDetailAsync(Guid userId, CancellationToken ct = default)
        {
            return await _ctx.UserDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.AppUserId == userId, ct);
        }

        public async Task UpsertUserDetailAsync(UserDetail detail, CancellationToken ct = default)
        {
            var existing = await _ctx.UserDetails.FirstOrDefaultAsync(d => d.AppUserId == detail.AppUserId, ct);
            if (existing == null)
            {
                detail.Id = detail.Id == Guid.Empty ? Guid.NewGuid() : detail.Id;
                detail.CreatedDate = detail.CreatedDate == default ? DateTime.UtcNow : detail.CreatedDate;
                detail.CreatedDate = DateTime.UtcNow;
                await _ctx.UserDetails.AddAsync(detail, ct);
            }
            else
            {
                existing.HeightCm = detail.HeightCm;
                existing.WeightKg = detail.WeightKg;
                existing.DateOfBirth = detail.DateOfBirth;
                existing.Gender = detail.Gender;
                // existing.ActivityLevel = detail.ActivityLevel;
                // existing.KnownConditions = detail.KnownConditions;
                // existing.RestingHeartRate = detail.RestingHeartRate;
                // existing.BloodPressure = detail.BloodPressure;
                // existing.UpdatedAt = DateTime.UtcNow;

                _ctx.UserDetails.Update(existing);
            }
            // Note: do NOT call SaveChanges here — caller (UnitOfWork / Service) will commit
        }

        // ---------------- Search / listing ----------------
        public async Task<(IEnumerable<AppUser> Users, int TotalCount)> SearchAsync(string? q, int page = 1, int pageSize = 20, CancellationToken ct = default)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                query = query.Where(u =>
                    EF.Functions.Like(u.Email, $"%{q}%") ||
                    EF.Functions.Like(u.UserName, $"%{q}%"));
                // EF.Functions.Like(u.DisplayName ?? string.Empty, $"%{q}%"))
            }

            var total = await query.CountAsync(ct);

            var users = await query
                .AsNoTracking()
                .OrderByDescending(u => u.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (users, total);
        }
        public async Task<AppUser?> FindByEmailOrUserNameAsync(string emailOrUserName, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(emailOrUserName)) return null;

            // Normalize for identity comparisons
            var normalized = emailOrUserName.Trim().ToUpperInvariant();

            // Try exact normalized match on normalized email or normalized username
            var user = await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.NormalizedEmail == normalized || u.NormalizedUserName == normalized, ct)
                .ConfigureAwait(false);

            if (user != null) return user;

            // Fallback: maybe user passed email with different casing or extra whitespace,
            // try direct equality on Email/UserName with case-insensitive compare via EF.Functions.Like
            // (This fallback is optional; above normalized check usually covers it.)
            user = await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => EF.Functions.Like(u.Email, emailOrUserName) || EF.Functions.Like(u.UserName, emailOrUserName), ct)
                .ConfigureAwait(false);

            return user;
        }
    }
}
