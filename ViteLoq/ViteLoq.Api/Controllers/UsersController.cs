using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViteLoq.Application.DTOs.UserManagement;
using ViteLoq.Application.Interfaces.UserManagement;
using ViteLoq.Application.Interfaces; // IEntryService (example)

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    // private readonly IEntryService _entryService; // service to get food/workout entries

    // public UsersController(IUserService userService, IEntryService entryService)
    public UsersController(IUserService userService)
    {
        _userService = userService;
        // _entryService = entryService;
    }

    // public registration endpoint (if you have one)
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var res = await _userService.CreateUserAsync(dto, ct);
        if (!res.Succeeded) return BadRequest(new { errors = res.Errors.Select(e => e.Description) });
        return CreatedAtAction(nameof(GetMe), null); // or return user id/location
    }

    // get current user's profile
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe(CancellationToken ct)
    {
        var sub = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(sub)) return Unauthorized();

        if (!Guid.TryParse(sub, out var userId)) return Unauthorized();

        var profile = await _userService.GetProfileAsync(userId, ct);
        if (profile == null) return Unauthorized();
        return Ok(profile);
    }

    // update profile
    [HttpPut("me")]
    [Authorize]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateProfileDto dto, CancellationToken ct)
    {
        var sub = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (!Guid.TryParse(sub, out var userId)) return Unauthorized();

        // force ownership
        dto.UserId = userId;

        await _userService.UpdateProfileAsync(dto, ct);
        return NoContent();
    }

    // get user's entries (food/workout) with optional filters
    
    // [HttpGet("me/entries")]
    // [Authorize]
    // public async Task<IActionResult> GetMyEntries([FromQuery] DateTime? from, [FromQuery] DateTime? to,
    //                                               [FromQuery] string? category, [FromQuery] int page = 1,
    //                                               [FromQuery] int pageSize = 50, CancellationToken ct = default)
    // {
    //     var sub = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
    //     if (!Guid.TryParse(sub, out var userId)) return Unauthorized();
    //
    //     // provide sane defaults
    //     var fromDt = from ?? DateTime.UtcNow.Date.AddDays(-7);
    //     var toDt = to ?? DateTime.UtcNow;
    //
    //     // example service returns paged DTOs or IEnumerable
    //     var (entries, total) = await _entryService.GetEntriesForUserAsync(userId, fromDt, toDt, category, page, pageSize, ct);
    //     return Ok(new { items = entries, total, page, pageSize });
    // }
    //
    // // get a single entry by id (food or workout)
    // [HttpGet("me/entries/{entryId:guid}")]
    // [Authorize]
    // public async Task<IActionResult> GetMyEntry(Guid entryId, CancellationToken ct)
    // {
    //     var sub = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
    //     if (!Guid.TryParse(sub, out var userId)) return Unauthorized();
    //
    //     var entry = await _entryService.GetEntryByIdAsync(userId, entryId, ct);
    //     if (entry == null) return NotFound();
    //     return Ok(entry);
    // }
}
