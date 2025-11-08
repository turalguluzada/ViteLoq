using Microsoft.AspNetCore.Identity;
using ViteLoq.Domain.Entities;

namespace ViteLoq.Domain.UserManagement.Interfaces;

public interface IUserRepository
{
    Task<AppUser?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AppUser?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<AppUser?> GetByUserNameAsync(string userName, CancellationToken ct = default);
    Task<IdentityResult> CreateAsync(AppUser user, string password, CancellationToken ct = default);
    Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken ct = default);
    Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword, CancellationToken ct = default);
    Task AddToRoleAsync(AppUser user, string role, CancellationToken ct = default);
    Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken ct = default);
}   