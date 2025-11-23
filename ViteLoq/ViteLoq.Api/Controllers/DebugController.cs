// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.AspNetCore.Mvc;
//
// namespace ViteLoq.Controllers;
//
// [ApiController]
// [Route("api/[controller]")]
// public class DebugController : ControllerBase
// {
//     [HttpGet("cookies")]
//     public IActionResult Cookies()
//     {
//         // Server can read HttpOnly cookies
//         var access = Request.Cookies["access_token"];
//         var refresh = Request.Cookies["refresh_token"];
//         return Ok(new { hasAccess = access != null, hasRefresh = refresh != null });
//     }
//
//     [HttpGet("tokeninfo")]
//     public IActionResult TokenInfo()
//     {
//         var access = Request.Cookies["access_token"];
//         if (string.IsNullOrEmpty(access)) return NotFound("access token cookie not found");
//
//         var handler = new JwtSecurityTokenHandler();
//         var token = handler.ReadJwtToken(access);
//         var claims = token.Claims.Select(c => new { c.Type, c.Value });
//         return Ok(new { claims });
//     }
// } 

using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace ViteLoq.Controllers;

[ApiController]
[Route("api/debug")]
public class DebugController : ControllerBase
{
    [HttpGet("cookies")]
    public IActionResult Cookies()
    {
        // Server can read HttpOnly cookies
        var access = Request.Cookies["access_token"];
        var refresh = Request.Cookies["refresh_token"];
        return Ok(new { hasAccess = access != null, hasRefresh = refresh != null });
    }

    [HttpGet("tokeninfo")]
    public IActionResult TokenInfo()
    {
        // Prefer Authorization header, fallback to cookie (useful for debug)
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        string token = null;
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            token = authHeader.Substring("Bearer ".Length).Trim();

        if (string.IsNullOrEmpty(token))
            token = Request.Cookies["access_token"];

        if (string.IsNullOrEmpty(token)) return NotFound("access token not found (cookie or Authorization header)");

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var claims = jwt.Claims.Select(c => new { c.Type, c.Value });
            return Ok(new { claims });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
