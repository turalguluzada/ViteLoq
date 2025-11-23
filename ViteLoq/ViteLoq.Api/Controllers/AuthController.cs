// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using ViteLoq.Application.DTOs.Auth;
// using ViteLoq.Application.Interfaces.Auth;
//
// namespace ViteLoq.Controllers
// {
//     [ApiController]
//     [Route("api/auth")]
//     public class AuthController : ControllerBase
//     {
//         private readonly IAuthService _auth;
//         public AuthController(IAuthService auth) => _auth = auth;
//
//         [HttpPost("login")]
//         public async Task<IActionResult> Login([FromBody] LoginDto dto)
//         {
//             dto.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
//
//             var tokens = await _auth.LoginAsync(dto);
//
//             // 1. Refresh token → HttpOnly cookie
//             Response.Cookies.Append("refresh_token", tokens.RefreshToken, new CookieOptions
//             {
//                 HttpOnly = true,
//                 Secure = true,                    // production-da true, dev-də false də olar
//                 SameSite = SameSiteMode.None,
//                 Expires = tokens.RefreshTokenExpiresAt,
//                 Path = "/"
//             });
//
//             // 2. XSRF token → JS oxusun deyə
//             Response.Cookies.Append("XSRF-TOKEN", Guid.NewGuid().ToString("N"), new CookieOptions
//             {
//                 HttpOnly = false,
//                 Secure = true,
//                 SameSite = SameSiteMode.None,
//                 Expires = tokens.AccessTokenExpiresAt,
//                 Path = "/"
//             });
//
//             // 3. Access token-i body-də qaytarırıq (frontend memory-də saxlayır)
//             return Ok(new
//             {
//                 accessToken = tokens.AccessToken,
//                 accessTokenExpiresAt = tokens.AccessTokenExpiresAt,
//                 refreshTokenExpiresAt = tokens.RefreshTokenExpiresAt
//             });
//         }
//
//         [HttpPost("refresh")]
//         public async Task<IActionResult> Refresh()
//         {
//             var refreshToken = Request.Cookies["refresh_token"];
//             if (string.IsNullOrEmpty(refreshToken))
//                 return Unauthorized(new { message = "Refresh token yoxdur" });
//
//             var dto = new RefreshRequestDto
//             {
//                 RefreshToken = refreshToken,
//                 IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
//             };
//
//             var result = await _auth.RefreshAsync(dto);
//             if (result == null)
//                 return Unauthorized(new { message = "Refresh token etibarsızdır" });
//
//             // Yenidən refresh_token cookie yenilə
//             Response.Cookies.Append("refresh_token", result.RefreshToken, new CookieOptions
//             {
//                 HttpOnly = true,
//                 Secure = true,
//                 SameSite = SameSiteMode.None,
//                 Expires = result.RefreshTokenExpiresAt,
//                 Path = "/"
//             });
//
//             // Yeni XSRF token
//             Response.Cookies.Append("XSRF-TOKEN", Guid.NewGuid().ToString("N"), new CookieOptions
//             {
//                 HttpOnly = false,
//                 Secure = true,
//                 SameSite = SameSiteMode.None,
//                 Expires = result.AccessTokenExpiresAt,
//                 Path = "/"
//             });
//
//             return Ok(new
//             {
//                 accessToken = result.AccessToken,
//                 accessTokenExpiresAt = result.AccessTokenExpiresAt,
//                 refreshTokenExpiresAt = result.RefreshTokenExpiresAt
//             });
//         }
//
//         [HttpPost("revoke")]
//         [Authorize] // Bearer token ilə işləyir
//         public async Task<IActionResult> Revoke()
//         {
//             var userIdClaim = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
//             if (!Guid.TryParse(userIdClaim, out var userId))
//                 return Unauthorized();
//
//             var refreshToken = Request.Cookies["refresh_token"];
//             if (!string.IsNullOrEmpty(refreshToken))
//             {
//                 await _auth.RevokeAsync(refreshToken, userId);
//             }
//
//             // Cookie-ləri sil
//             Response.Cookies.Delete("refresh_token", new CookieOptions { Path = "/" });
//             Response.Cookies.Delete("XSRF-TOKEN", new CookieOptions { Path = "/" });
//
//             return NoContent();
//         }
//     }
// }

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViteLoq.Application.DTOs.Auth;
using ViteLoq.Application.Interfaces.Auth;

namespace ViteLoq.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        // Helper: build cookie options consistently
        private CookieOptions BuildRefreshCookieOptions(DateTimeOffset expires)
        {
            var secure = Request.IsHttps;
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = secure,
                SameSite = SameSiteMode.None,
                Expires = expires.UtcDateTime,
                Path = "/"
            };
        }

        private CookieOptions BuildXsrfCookieOptions(DateTimeOffset expires)
        {
            var secure = Request.IsHttps;
            return new CookieOptions
            {
                HttpOnly = false,          // JS should read it
                Secure = secure,
                SameSite = SameSiteMode.None,
                Expires = expires.UtcDateTime,
                Path = "/"
            };
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            dto.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var tokens = await _auth.LoginAsync(dto);
            if (tokens == null)
                return Unauthorized(new { message = "Invalid credentials" });

            // 1) Refresh token -> HttpOnly cookie
            Response.Cookies.Append("refresh_token", tokens.RefreshToken, BuildRefreshCookieOptions(tokens.RefreshTokenExpiresAt));

            // 2) XSRF token -> JS oxusun
            var xsrf = Guid.NewGuid().ToString("N");
            Response.Cookies.Append("XSRF-TOKEN", xsrf, BuildXsrfCookieOptions(tokens.AccessTokenExpiresAt));

            // 3) Return access token in body (frontend stores in memory and uses Authorization header)
            return Ok(new
            {
                accessToken = tokens.AccessToken,
                accessTokenExpiresAt = tokens.AccessTokenExpiresAt,
                refreshTokenExpiresAt = tokens.RefreshTokenExpiresAt
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refresh_token"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized(new { message = "Refresh token yoxdur" });

            var dto = new RefreshRequestDto
            {
                RefreshToken = refreshToken,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
            };

            var result = await _auth.RefreshAsync(dto);
            if (result == null)
                return Unauthorized(new { message = "Refresh token etibarsızdır" });

            // rotate: yenə HttpOnly refresh cookie set et
            Response.Cookies.Append("refresh_token", result.RefreshToken, BuildRefreshCookieOptions(result.RefreshTokenExpiresAt));

            // yeni XSRF token (JS yenisini oxuyub header-ə qoyacaq)
            var xsrf = Guid.NewGuid().ToString("N");
            Response.Cookies.Append("XSRF-TOKEN", xsrf, BuildXsrfCookieOptions(result.AccessTokenExpiresAt));

            return Ok(new
            {
                accessToken = result.AccessToken,
                accessTokenExpiresAt = result.AccessTokenExpiresAt,
                refreshTokenExpiresAt = result.RefreshTokenExpiresAt
            });
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            var userIdClaim = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var refreshToken = Request.Cookies["refresh_token"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _auth.RevokeAsync(refreshToken, userId);
            }

            // delete cookie(s) — use same Path/SameSite/Secure so browser actually removes them
            var secure = Request.IsHttps;
            var deleteRefresh = new CookieOptions { Path = "/", SameSite = SameSiteMode.None, Secure = secure };
            var deleteXsrf = new CookieOptions { Path = "/", SameSite = SameSiteMode.None, Secure = secure };

            Response.Cookies.Delete("refresh_token", deleteRefresh);
            Response.Cookies.Delete("XSRF-TOKEN", deleteXsrf);

            return NoContent();
        }

        
    }
}
