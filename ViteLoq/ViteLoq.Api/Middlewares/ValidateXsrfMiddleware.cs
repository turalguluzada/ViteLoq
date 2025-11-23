using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ViteLoq.Api.Middlewares;
public class ValidateXsrfMiddleware
{
    private readonly RequestDelegate _next;
    public ValidateXsrfMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext ctx)
    {
        if (ctx.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase))
        {
            if (!(HttpMethods.IsGet(ctx.Request.Method) ||
                  HttpMethods.IsHead(ctx.Request.Method) ||
                  HttpMethods.IsOptions(ctx.Request.Method)))
            {
                // Allow endpoints explicitly marked with [AllowAnonymous]
                var endpoint = ctx.GetEndpoint();
                if (endpoint != null && endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
                {
                    await _next(ctx);
                    return;
                }

                // If the current request is from an anonymous (unauthenticated) user,
                // skip XSRF check — typical for register/login endpoints.
                if (!(ctx.User?.Identity?.IsAuthenticated ?? false))
                {
                    await _next(ctx);
                    return;
                }

                // Otherwise enforce double-submit: cookie vs header
                if (!ctx.Request.Cookies.TryGetValue("XSRF-TOKEN", out var cookieVal) ||
                    !ctx.Request.Headers.TryGetValue("X-XSRF-TOKEN", out var headerVal) ||
                    string.IsNullOrEmpty(headerVal) ||
                    !string.Equals(cookieVal, headerVal.ToString(), StringComparison.Ordinal))
                {
                    ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await ctx.Response.WriteAsync("Invalid or missing XSRF token");
                    return;
                }
            }
        }

        await _next(ctx);
    }
}

// public class ValidateXsrfMiddleware
// {
//     private readonly RequestDelegate _next;
//     public ValidateXsrfMiddleware(RequestDelegate next) => _next = next;
//
//     public async Task InvokeAsync(HttpContext ctx)
//     {
//         // Only protect API endpoints
//         if (ctx.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase))
//         {
//             // safe (idempotent) methods we don't check
//             if (!(HttpMethods.IsGet(ctx.Request.Method) ||
//                   HttpMethods.IsHead(ctx.Request.Method) ||
//                   HttpMethods.IsOptions(ctx.Request.Method)))
//             {
//                 // Allow specific endpoints (login already allowed)
//                 if (ctx.Request.Path.StartsWithSegments("/api/auth/login", StringComparison.OrdinalIgnoreCase))
//                 {
//                     await _next(ctx);
//                     return;
//                 }
//
//                 // ALSO allow registration and other anonymous write endpoints automatically
//                 // if the endpoint has [AllowAnonymous] attribute, skip XSRF check:
//                 var endpoint = ctx.GetEndpoint();
//                 if (endpoint != null)
//                 {
//                     var allowAnon = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>();
//                     if (allowAnon != null)
//                     {
//                         await _next(ctx);
//                         return;
//                     }
//                 }
//
//                 // Fallback: if you want to allow specific paths, add checks:
//                 if (ctx.Request.Path.StartsWithSegments("/api/v1/users", StringComparison.OrdinalIgnoreCase)
//                     && HttpMethods.IsPost(ctx.Request.Method))
//                 {
//                     // registration endpoint — skip XSRF (optional)
//                     await _next(ctx);
//                     return;
//                 }
//
//                 // For all other state-changing requests require XSRF header & cookie
//                 if (!ctx.Request.Cookies.TryGetValue("XSRF-TOKEN", out var cookieVal) ||
//                     !ctx.Request.Headers.TryGetValue("X-XSRF-TOKEN", out var headerVal) ||
//                     string.IsNullOrEmpty(headerVal) ||
//                     !string.Equals(cookieVal, headerVal.ToString(), StringComparison.Ordinal))
//                 {
//                     ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
//                     await ctx.Response.WriteAsync("Invalid or missing XSRF token");
//                     return;
//                 }
//             }
//         }
//
//         await _next(ctx);
//     }
// }
