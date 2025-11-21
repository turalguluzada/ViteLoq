namespace ViteLoq.Api.Middlewares;

public class ValidateXsrfMiddleware
{
    private readonly RequestDelegate _next;
    public ValidateXsrfMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext ctx)
    {
        // Only protect API endpoints
        if (ctx.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase))
        {
            // safe (idempotent) methods we don't check
            if (!(HttpMethods.IsGet(ctx.Request.Method) ||
                  HttpMethods.IsHead(ctx.Request.Method) ||
                  HttpMethods.IsOptions(ctx.Request.Method)))
            {
                // Allow login endpoint without XSRF (login has no XSRF cookie yet)
                if (ctx.Request.Path.StartsWithSegments("/api/auth/login", StringComparison.OrdinalIgnoreCase))
                {
                    await _next(ctx);
                    return;
                }

                // For all other state-changing requests require XSRF header & cookie
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