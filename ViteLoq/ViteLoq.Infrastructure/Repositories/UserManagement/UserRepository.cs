using Microsoft.AspNetCore.Identity;
using ViteLoq.Domain.Entities;
using ViteLoq.Domain.UserManagement.Interfaces;
using ViteLoq.Infrastructure.Persistence;

namespace ViteLoq.Infrastructure.Repositories.UserManagement;

public class UserRepository : IUserRepository
{
    private readonly ViteLoqDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public UserRepository(ViteLoqDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<AppUser?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return user;
    }

    public async Task<AppUser?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }

    public async Task<AppUser?> GetByUserNameAsync(string userName, CancellationToken ct = default)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user;
    }

    public async Task<IdentityResult> CreateAsync(AppUser user, string password, CancellationToken ct = default)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result;
    }

    public async Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken ct = default)
    {
        var result = await _userManager.UpdateAsync(user);
        return result;
    }

    public async Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword, CancellationToken ct = default)
    {
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result;
    }

    public async Task AddToRoleAsync(AppUser user, string role, CancellationToken ct = default)
    {
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken ct = default)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles;
    }
}