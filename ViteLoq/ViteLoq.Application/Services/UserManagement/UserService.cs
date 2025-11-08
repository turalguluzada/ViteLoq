using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ViteLoq.Application.DTOs.UserManagement;
using ViteLoq.Application.Interfaces.UserManagement;
using ViteLoq.Domain.Entities;
using ViteLoq.Domain.Interfaces;
using ViteLoq.Domain.UserManagement.Interfaces;

namespace ViteLoq.Application.Services.UserManagement;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IdentityResult> CreateUserAsync(CreateUserDto dto, CancellationToken ct = default)
    {
        AppUser user = new AppUser();
        user.UserName = dto.UserName;
        user.Email = dto.Email;
        user.EmailConfirmed = true;
        user.PhoneNumberConfirmed = true;
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        
        var result = await _userRepository.CreateAsync(user, dto.Password);
        return result;
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(Guid userId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckPasswordAsync(Guid userId, string password, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto dto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserProfileDto?> GetProfileAsync(Guid userId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateProfileAsync(UpdateProfileDto dto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<string>> GetRolesAsync(Guid userId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task AddToRoleAsync(Guid userId, string role, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Claim>> GetClaimsAsync(Guid userId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserProfileDto> SearchUsersAsync(string? q, int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpsertUserDetailAsync(UserDetailDto dto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<AppUserDto?> GetBasicUserForSystemAsync(Guid userId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}