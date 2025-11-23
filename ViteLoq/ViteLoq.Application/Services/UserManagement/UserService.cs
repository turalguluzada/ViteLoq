using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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
    private readonly IUnitOfWork? _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IUnitOfWork? unitOfWork, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _unitOfWork = unitOfWork;
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IdentityResult> CreateUserAsync(CreateUserDto dto, CancellationToken ct = default)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        // map DTO -> AppUser
        AppUser user = _mapper.Map<AppUser>(dto);
        user.Id = Guid.NewGuid();
        user.CreatedDate = DateTime.UtcNow;
        // Ensure email normalization if you rely on it
        user.NormalizedEmail = user.Email?.ToUpperInvariant();
        user.NormalizedUserName = user.UserName?.ToUpperInvariant();

        // repository handles UserManager internals and persist user
        
        var result = await _userRepository.CreateAsync(user, dto.Password).ConfigureAwait(false);

        // var result = await _userRepository.CreateAsync(user, dto.Password, ct).ConfigureAwait(false);

        // if repo created user and there is initial detail, persist it
        if (result.Succeeded && dto.InitialProfile != null)
        {
            var detail = _mapper.Map<UserDetail>(dto.InitialProfile);
            detail.Id = Guid.NewGuid();
            detail.AppUserId = user.Id;
            detail.CreatedDate = DateTime.UtcNow;
            detail.UpdatedDate = DateTime.UtcNow;

            await _userRepository.UpsertUserDetailAsync(detail, ct).ConfigureAwait(false);
            if (_unitOfWork != null)
                await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
        }

        return result;
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct).ConfigureAwait(false);
        if (user == null) throw new InvalidOperationException("User not found");
        return await _userRepository.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
    }

    public async Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct).ConfigureAwait(false);
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        var res = await _userRepository.ConfirmEmailAsync(user, token, ct).ConfigureAwait(false);
        return res;
    }

    public async Task<bool> CheckPasswordAsync(Guid userId, string password, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct).ConfigureAwait(false);
        if (user == null) return false;
        return await _userRepository.CheckPasswordAsync(user, password, ct).ConfigureAwait(false);
    }

    public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto dto, CancellationToken ct = default)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        var user = await _userRepository.GetByIdAsync(dto.UserId, ct).ConfigureAwait(false);
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        var res = await _userRepository.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword, ct).ConfigureAwait(false);
        return res;
    }

    public async Task<UserProfileDto?> GetProfileAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct).ConfigureAwait(false);
        if (user == null) return null;

        // var detailTask = _userRepository.GetUserDetailAsync(userId, ct);
        // var rolesTask = _userRepository.GetRolesAsync(user, ct);
        // var claimsTask = _userRepository.GetClaimsAsync(user, ct);
        //
        // await Task.WhenAll(detailTask, rolesTask, claimsTask).ConfigureAwait(false);
        
        var detailTask = await _userRepository.GetUserDetailAsync(userId, ct);
        var rolesTask = await _userRepository.GetRolesAsync(user, ct);
        var claimsTask = await _userRepository.GetClaimsAsync(user, ct);

        UserProfileDto profile = null;
        try
        {
            profile = _mapper.Map<UserProfileDto>(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine("MAPPER ERROR: " + ex.Message);
            throw; // vacibdir, yoxsa debugger sətri “keçir”
        }
        
        // var profile = new UserProfileDto();
        // profile.UserId = user.Id;
        // profile.UserName = user.UserName;
        // profile.Email = user.Email;
        // profile.FirstName = user.FirstName;
        // profile.LastName = user.LastName;
        // profile.PhoneNumber = user.PhoneNumber;
        // profile.EmailConfirmed = user.EmailConfirmed;
        // profile.DisplayName = user.DisplayName;
        
        // var detail = detailTask.Result;
        var detail = detailTask;
        if (detail != null) profile.Detail = _mapper.Map<UserDetailDto>(detail);
        // profile.Roles = rolesTask.Result ?? Array.Empty<string>();
        // profile.Claims = (claimsTask.Result ?? Enumerable.Empty<Claim>()).Select(c => new UserClaimDto { Type = c.Type, Value = c.Value }).ToArray();
        profile.Claims = (claimsTask ?? Enumerable.Empty<Claim>()).Select(c => new UserClaimDto { Type = c.Type, Value = c.Value }).ToArray();
        return profile;
    }

    public async Task UpdateProfileAsync(UpdateProfileDto dto, CancellationToken ct = default)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        // Controller should set dto.UserId from token — double-check here
        var user = await _userRepository.GetByIdAsync(dto.UserId, ct).ConfigureAwait(false);
        if (user == null) throw new InvalidOperationException("User not found");

        var updated = false;

        if (!string.IsNullOrWhiteSpace(dto.DisplayName) && dto.DisplayName != user.DisplayName)
        {
            user.DisplayName = dto.DisplayName.Trim();
            var identityRes = await _userRepository.UpdateAsync(user, ct).ConfigureAwait(false);
            if (!identityRes.Succeeded)
            {
                var msg = string.Join("; ", identityRes.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to update user: {msg}");
            }
            updated = true;
        }

        // Upsert detail
        var detail = _mapper.Map<UserDetail>(dto);
        detail.AppUserId = dto.UserId;
        detail.UpdatedDate = DateTime.UtcNow;

        await _userRepository.UpsertUserDetailAsync(detail, ct).ConfigureAwait(false);
        if (_unitOfWork != null)
            await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);

        // nothing to return (void). If you want OperationResult, change signature.
    }

    public async Task<IList<string>> GetRolesAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct).ConfigureAwait(false);
        if (user == null) return Array.Empty<string>();
        return await _userRepository.GetRolesAsync(user, ct).ConfigureAwait(false);
    }

    public async Task AddToRoleAsync(Guid userId, string role, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct).ConfigureAwait(false);
        if (user == null) throw new InvalidOperationException("User not found");

        await _userRepository.AddToRoleAsync(user, role, ct).ConfigureAwait(false);
        if (_unitOfWork != null)
            await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
    }

    public async Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct).ConfigureAwait(false);
        if (user == null) return false;
        return await _userRepository.IsInRoleAsync(user, role, ct).ConfigureAwait(false);
    }

    public async Task<IList<Claim>> GetClaimsAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct).ConfigureAwait(false);
        if (user == null) return new List<Claim>();
        return await _userRepository.GetClaimsAsync(user, ct).ConfigureAwait(false);
    }

    public async Task<UserProfileDto> SearchUsersAsync(string? q, int page = 1, int pageSize = 20, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// NOTE: your method signature returns a single UserProfileDto.
    /// Typically search returns many results (paged). Here we return the first match mapped into UserProfileDto.
    /// Consider changing signature to PagedResult<UserProfileDto> later.
    /// </summary>
    // public async Task<UserProfileDto> SearchUsersAsync(string? q, int page = 1, int pageSize = 20, CancellationToken ct = default)
    // {
    //     // Expect repository to provide a search method that returns (IEnumerable<AppUser>, total) or IEnumerable<AppUser>.
    //     // Try to call the tuple-returning version (common pattern). If your repo differs, adapt.
    //     var searchResult = await _userRepository.SearchAsync(q, page, pageSize, ct).ConfigureAwait(false);
    //     // If SearchAsync returned a tuple (users, total)
    //     if (searchResult is ValueTuple<IEnumerable<AppUser>, int> tuple)
    //     {
    //         var users = tuple.Item1;
    //         var first = users.FirstOrDefault();
    //         if (first == null) return new UserProfileDto();
    //         var profile = _mapper.Map<UserProfileDto>(first);
    //         var detail = await _userRepository.GetUserDetailAsync(first.Id, ct).ConfigureAwait(false);
    //         if (detail != null) profile.Detail = _mapper.Map<UserDetailDto>(detail);
    //         profile.Roles = await _userRepository.GetRolesAsync(first, ct).ConfigureAwait(false) ?? Array.Empty<string>();
    //         profile.Claims = (await _userRepository.GetClaimsAsync(first, ct).ConfigureAwait(false) ?? Enumerable.Empty<Claim>())
    //                             .Select(c => new UserClaimDto { Type = c.Type, Value = c.Value }).ToArray();
    //         return profile;
    //     }
    //
    //     // If SearchAsync returned just IEnumerable<AppUser>
    //     if (searchResult is IEnumerable<AppUser> usersEnumerable)
    //     {
    //         var first = usersEnumerable.FirstOrDefault();
    //         if (first == null) return new UserProfileDto();
    //         var profile = _mapper.Map<UserProfileDto>(first);
    //         var detail = await _userRepository.GetUserDetailAsync(first.Id, ct).ConfigureAwait(false);
    //         if (detail != null) profile.Detail = _mapper.Map<UserDetailDto>(detail);
    //         profile.Roles = await _userRepository.GetRolesAsync(first, ct).ConfigureAwait(false) ?? Array.Empty<string>();
    //         profile.Claims = (await _userRepository.GetClaimsAsync(first, ct).ConfigureAwait(false) ?? Enumerable.Empty<Claim>())
    //                             .Select(c => new UserClaimDto { Type = c.Type, Value = c.Value }).ToArray();
    //         return profile;
    //     }
    //
    //     // Fallback — if repository returns something else, try best-effort
    //     return new UserProfileDto();
    // }

    public async Task UpsertUserDetailAsync(UserDetailDto dto, CancellationToken ct = default)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        var user = await _userRepository.GetByIdAsync(dto.UserId, ct).ConfigureAwait(false);
        if (user == null) throw new InvalidOperationException("User not found");

        var detail = _mapper.Map<UserDetail>(dto);
        detail.UpdatedDate = DateTime.UtcNow;
        await _userRepository.UpsertUserDetailAsync(detail, ct).ConfigureAwait(false);

        if (_unitOfWork != null)
            await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
    }

    public async Task<AppUserDto?> GetBasicUserForSystemAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, ct).ConfigureAwait(false);
        if (user == null) return null;
        return _mapper.Map<AppUserDto>(user);
    }
}
