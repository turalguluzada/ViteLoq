using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ViteLoq.Domain.Entities;

namespace ViteLoq.Domain.UserManagement.Interfaces
{
    public interface IUserRepository
    {
        // Identity / user lifecycle
        Task<IdentityResult> CreateAsync(AppUser user, string password, IEnumerable<string>? roles = null, CancellationToken ct = default);
        Task<AppUser?> GetByIdAsync(Guid userId, CancellationToken ct = default);
        Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken ct = default);

        // Email / tokens
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user, CancellationToken ct = default);
        Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token, CancellationToken ct = default);

        // Authentication helpers
        Task<bool> CheckPasswordAsync(AppUser user, string password, CancellationToken ct = default);
        Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword, CancellationToken ct = default);

        // Roles & claims
        Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken ct = default);
        Task AddToRoleAsync(AppUser user, string role, CancellationToken ct = default);
        Task<bool> IsInRoleAsync(AppUser user, string role, CancellationToken ct = default);
        Task<IList<Claim>> GetClaimsAsync(AppUser user, CancellationToken ct = default);

        // User detail (profile) persistence
        Task UpsertUserDetailAsync(UserDetail detail, CancellationToken ct = default);
        Task<UserDetail?> GetUserDetailAsync(Guid userId, CancellationToken ct = default);

        // Search / listing (returns items + total count)
        Task<(IEnumerable<AppUser> Users, int TotalCount)> SearchAsync(string? q, int page = 1, int pageSize = 20, CancellationToken ct = default);
        Task<AppUser?> FindByEmailOrUserNameAsync(string emailOrUserName, CancellationToken ct = default);
    }
}