using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViteLoq.Application.DTOs.UserManagement;
using ViteLoq.Application.Interfaces.UserManagement;
using AutoMapper;
using ViteLoq.Application.DTOs.Entry;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ViteLoq.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        // private readonly IEntryService _entryService; // returns user's entries (nutrition/workout)
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserService userService,
            // IEntryService entryService,
            IMapper mapper,
            ILogger<UsersController> logger)
        {
            _userService = userService;
            // _entryService = entryService;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet("test")]
        public IActionResult Test()
        {
            var headers = Request.Headers.Select(h => new { Key = h.Key, Value = h.Value.ToString() }).ToList();
            var cookies = Request.Cookies.Select(c => new { Key = c.Key, Value = c.Value }).ToList();
            var claims = (User?.Claims ?? Enumerable.Empty<Claim>())
                .Select(c => new { c.Type, c.Value })
                .ToList();

            _logger.LogInformation("Request headers: {@headers}", headers);
            _logger.LogInformation("Request cookies: {@cookies}", cookies);
            _logger.LogInformation("User authenticated: {auth}", User?.Identity?.IsAuthenticated ?? false);
            _logger.LogInformation("User claims: {@claims}", claims);

            return Ok(new
            {
                auth = User?.Identity?.IsAuthenticated ?? false,
                headers,
                cookies,
                claims
            });
        }

        // ---------- Registration ----------
        /// <summary>Create a new user (registration).</summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _userService.CreateUserAsync(dto, ct);
            if (!result.Succeeded)
            {
                // standardize errors (application layer can return well-shaped failures)
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { errors });
            }

            // Option 1: return Created with location of profile (GetMe). If your CreateUser returns id, use it.
            // return CreatedAtAction(nameof(GetById), new { id = result }, result);
            return Ok();

        }

        // ---------- Read current user ----------
        /// <summary>Get currently authenticated user's profile.</summary>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(typeof(UserProfileDto), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetMe(CancellationToken ct)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized();

            var profile = await _userService.GetProfileAsync(userId, ct);
            if (profile == null) return NotFound(); // or Unauthorized depending on policy
            return Ok(profile);
        }

        // ---------- Get user by id (public / admin) ----------
        /// <summary>Get user by id (admin or internal use).</summary>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")] // or custom policy
        [ProducesResponseType(typeof(UserProfileDto), 200)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var profile = await _userService.GetProfileAsync(id, ct);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        // ---------- Update profile (current user) ----------
        /// <summary>Update current user's profile.</summary>
        [HttpPut("me")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateMe([FromBody] UpdateProfileDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized();

            // Enforce ownership server-side
            dto.UserId = userId;

            await _userService.UpdateProfileAsync(dto, ct);
            return NoContent();
        }

        // ---------- Get my entries (paginated & filtered) ----------
        /// <summary>Get user's entries (nutrition/workout) with optional filters and pagination.</summary>
        [HttpGet("me/entries")]
        [Authorize]
        [ProducesResponseType(typeof(PagedResult<UserEntryDto>), 200)]
        public async Task<IActionResult> GetMyEntries(
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null,
            [FromQuery] string? category = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            CancellationToken ct = default)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized();

            // sane defaults
            var fromDt = from ?? DateTime.UtcNow.Date.AddDays(-7);
            var toDt = to ?? DateTime.UtcNow;

            // service returns a PagedResult<UserEntryDto>
            // var paged = await _entryService.GetEntriesForUserAsync(userId, fromDt, toDt, category, page, pageSize, ct);

            // return Ok(paged);
            return Ok();
        }

        // ---------- Get single entry ----------
        /// <summary>Get a single user entry by id (belongs to the current user).</summary>
        [HttpGet("me/entries/{entryId:guid}")]
        [Authorize]
        [ProducesResponseType(typeof(UserEntryDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMyEntry(Guid entryId, CancellationToken ct)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized();

            // var entry = await _entryService.GetEntryByIdAsync(userId, entryId, ct);
            // if (entry == null) return NotFound();
            // return Ok(entry);
            return Ok();
        }

        // ---------- Admin search users ----------
        /// <summary>Search users (admin)</summary>
        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(PagedResult<UserProfileDto>), 200)]
        public async Task<IActionResult> SearchUsers([FromQuery] string? q = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        {
            var result = await _userService.SearchUsersAsync(q, page, pageSize, ct);
            return Ok(result);
        }
        
        // private Guid GetUserIdFromClaims()
        // {
        //     Guid userId = Guid.Parse("745d1b0f-1a56-4643-a655-08de1ecf45d6");
        //     return userId;
        //     
        //     var sub = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        //     if (string.IsNullOrEmpty(sub)) return Guid.Empty;
        //     return Guid.TryParse(sub, out var id) ? id : Guid.Empty;
        // }
        // ---------- Helpers ----------
        private Guid GetUserId()
        {
            var candidates = new[] { JwtRegisteredClaimNames.Sub, ClaimTypes.NameIdentifier, "userId", "id" };

            foreach (var t in candidates)
            {
                var val = User.FindFirstValue(t);
                if (!string.IsNullOrEmpty(val) && Guid.TryParse(val, out var g))
                    return g;
            }

            return Guid.Empty;
        }
    }

    // ----------------------
    // Small support types you likely have in Application layer.
    // Put these in ViteLoq.Application.DTOs.* files, not inside controller file.
    // ----------------------

    public record PagedResult<T>(IEnumerable<T> Items, int Total, int Page, int PageSize);

    // Example DTO signatures (implement in Application.DTOs.UserManagement / Nutrition)
    // public class UserProfileDto { public Guid Id { get; set; } public string UserName { get; set; } ... }
    // public class UserEntryDto { public Guid Id { get; set; } public DateTime DateTime { get; set; } ... }
}
