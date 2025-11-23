using Microsoft.AspNetCore.Identity;
using ViteLoq.Application.DTOs.UserManagement;
using ViteLoq.Domain.Entities;

namespace ViteLoq.Application.Interfaces.UserManagement
{
    public interface IUserService
    {
        // ---- Creation / registration ----
        /// <summary>
        /// Create new user (register). Returns IdentityResult so controller can map errors.
        /// Typically service will call IUserRepository.CreateAsync and optionally seed profile.
        /// </summary>
        Task<IdentityResult> CreateUserAsync(CreateUserDto dto, CancellationToken ct = default);

        // ---- Auth / account management ----
        /// <summary>
        /// Generates email confirmation token for given userId (for email workflows).
        /// </summary>
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId, CancellationToken ct = default);

        Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token, CancellationToken ct = default);

        /// <summary>
        /// Verify user's password (returns true if matches). Useful for custom flows.
        /// </summary>
        Task<bool> CheckPasswordAsync(Guid userId, string password, CancellationToken ct = default);

        /// <summary>
        /// Change password for the user (current password verification included).
        /// </summary>
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto dto, CancellationToken ct = default);

        // ---- Profile / details ----
        /// <summary>
        /// Get full profile for a user (used by /api/users/me).
        /// </summary>
        Task<UserProfileDto?> GetProfileAsync(Guid userId, CancellationToken ct = default);

        /// <summary>
        /// Update profile (server will ensure userId ownership / override from token).
        /// </summary>
        /// 
        // Task<OperationResult> UpdateProfileAsync(UpdateProfileDto dto, CancellationToken ct = default);
        Task UpdateProfileAsync(UpdateProfileDto dto, CancellationToken ct = default);

        // ---- Roles & claims ----
        Task<IList<string>> GetRolesAsync(Guid userId, CancellationToken ct = default);
        Task AddToRoleAsync(Guid userId, string role, CancellationToken ct = default);
        // Task<OperationResult> AddToRoleAsync(Guid userId, string role, CancellationToken ct = default);

        Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken ct = default);
        Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(Guid userId, CancellationToken ct = default);

        // ---- Admin / listing ----
        /// <summary>
        /// Search users for admin UI with paging. Returns DTO list + total count.
        /// </summary>
        /// 
        // Task<PagedResult<UserProfileDto>> SearchUsersAsync(string? q, int page = 1, int pageSize = 20, CancellationToken ct = default);
        Task<UserProfileDto> SearchUsersAsync(string? q, int page = 1, int pageSize = 20, CancellationToken ct = default);

        // ---- Profile detail upsert (if app allows separate detail save) ----
        // Task<OperationResult> UpsertUserDetailAsync(UserDetailDto dto, CancellationToken ct = default);
        Task UpsertUserDetailAsync(UserDetailDto dto, CancellationToken ct = default);

        // ---- Utility ----
        // Task<AppUserDto?> GetBasicUserForSystemAsync(Guid userId, CancellationToken ct = default); // minimal info for internal use
        Task<AppUserDto?> GetBasicUserForSystemAsync(Guid userId, CancellationToken ct = default); // minimal info for internal use

    }
}