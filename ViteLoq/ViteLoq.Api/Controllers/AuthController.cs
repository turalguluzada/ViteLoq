using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViteLoq.Application.DTOs.Auth;
using ViteLoq.Application.Interfaces.Auth;

namespace ViteLoq.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth) => _auth = auth;
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            dto.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            try
            {
                var tokens = await _auth.LoginAsync(dto);

                var cookieOpts = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None, // cross-origin SPA üçün; eyni origin üçün Lax/Strict istifadə et
                    Expires = tokens.RefreshTokenExpiresAt,
                    Path = "/"
                };
                // refresh cookie: store plain token in cookie (DB stores its hash)
                Response.Cookies.Append("refresh_token", tokens.RefreshToken, cookieOpts);

                // access token cookie (optional). If you prefer access-in-memory, skip setting this.
                Response.Cookies.Append("access_token", tokens.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = tokens.AccessTokenExpiresAt,
                    Path = "/"
                });

                // set non-HttpOnly XSRF cookie for double-submit CSRF protection
                var xsrf = Guid.NewGuid().ToString("N");
                Response.Cookies.Append("XSRF-TOKEN", xsrf, new CookieOptions
                {
                    HttpOnly = false, // readable by JS
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = tokens.AccessTokenExpiresAt,
                    Path = "/"
                });

                // map or return minimal info
                return Ok(new
                {
                    accessTokenExpiresAt = tokens.AccessTokenExpiresAt,
                    refreshTokenExpiresAt = tokens.RefreshTokenExpiresAt
                });
            }
            catch (InvalidOperationException)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            // we expect refresh cookie present; no body needed
            var cookie = Request.Cookies["refresh_token"];
            if (string.IsNullOrEmpty(cookie)) return Unauthorized();

            var dto = new RefreshRequestDto
                { RefreshToken = cookie, IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() };
            var res = await _auth.RefreshAsync(dto);
            if (res == null) return Unauthorized();

            // replace cookies
            Response.Cookies.Append("access_token", res.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = res.AccessTokenExpiresAt,
                    Path = "/"
                });
            Response.Cookies.Append("refresh_token", res.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = res.RefreshTokenExpiresAt,
                    Path = "/"
                });

            return Ok(new
                { accessTokenExpiresAt = res.AccessTokenExpiresAt, refreshTokenExpiresAt = res.RefreshTokenExpiresAt });
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke([FromBody] object? body) // body optional
        {
            var userId = Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
            var cookie = Request.Cookies["refresh_token"];
            if (!string.IsNullOrEmpty(cookie))
            {
                await _auth.RevokeAsync(cookie, userId);
            }

            // delete cookies client-side
            Response.Cookies.Delete("access_token", new CookieOptions { Path = "/" });
            Response.Cookies.Delete("refresh_token", new CookieOptions { Path = "/" });

            return NoContent();
        }
    }
}