using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ViteLoq.Application.Mappings;
using ViteLoq.Api.Middlewares;
using ViteLoq.Application.Interfaces.Auth;
using ViteLoq.Application.Interfaces.Templates;
using ViteLoq.Application.Interfaces.UserManagement;
using ViteLoq.Application.Services.Auth;
using ViteLoq.Domain.Entities;
using ViteLoq.Domain.Interfaces;
using ViteLoq.Domain.Token.Interfaces;
using ViteLoq.Infrastructure.Persistence;
using ViteLoq.Infrastructure.Repositories.Auth;
using ViteLoq.Infrastructure.Repositories.Templates;
using ViteLoq.Infrastructure.Repositories.UserManagement;
using ViteLoq.Application.Services;
using ViteLoq.Application.Services.Templates;
using ViteLoq.Application.Services.UserManagement;
using ViteLoq.Domain.Templates.Interfaces;
using ViteLoq.Domain.UserManagement.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ========== Configuration ==========
var configuration = builder.Configuration;
var env = builder.Environment;

var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key missing");
var jwtIssuer = configuration["Jwt:Issuer"];
var jwtAudience = configuration["Jwt:Audience"];

// ========== Services ==========

// EF Core + Identity
builder.Services.AddDbContext<ViteLoqDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ViteLoqDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
});

// CORS: one canonical set of policies
builder.Services.AddCors(options =>
{
    // Prod / explicit SPA origins
    options.AddPolicy("AllowVite", policy =>
    {
        policy.WithOrigins("https://localhost:5173", "http://localhost:5173") // <- change to your frontend origin(s)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });

    // Dev: very permissive (use only for local dev)
    options.AddPolicy("Dev", policy =>
    {
        policy.SetIsOriginAllowed(origin => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// FluentValidation + Controllers
builder.Services.AddControllers()
    .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Program>());

// AutoMapper
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// var assemblyName = typeof(UserMappingProfile).Assembly.GetName().Name;
// Console.WriteLine("Mapping assembly: " + assemblyName);

builder.Services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
// Application / Infrastructure DI
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

builder.Services.AddScoped<INutritionItemRepository, NutritionItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<INutritionItemService, NutritionItemService>();
builder.Services.AddScoped<IUserService, UserService>();


// UnitOfWork: resolve IUnitOfWork to DbContext instance
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ViteLoqDbContext>());

// other application services
builder.Services.AddScoped<EntryService>();

// JWT Authentication
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // RequireHttpsMetadata true in production; false for local dev if needed
    options.RequireHttpsMetadata = env.IsProduction();
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = !string.IsNullOrEmpty(jwtIssuer),
        ValidIssuer = jwtIssuer,
        ValidateAudience = !string.IsNullOrEmpty(jwtAudience),
        ValidAudience = jwtAudience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(30)
    };

    // allow reading access token from cookie if Authorization header not provided
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader)) return Task.CompletedTask;

            if (context.Request.Cookies.TryGetValue("access_token", out var tokenFromCookie))
            {
                context.Token = tokenFromCookie;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

// Swagger only in development
if (env.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Dev cert hint
// Instruct devs to run: dotnet dev-certs https --trust

// Pipeline
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// serve demo/static files if present
app.UseStaticFiles();

app.UseRouting();

// choose CORS policy by environment (Dev vs Prod)
var corsPolicy = env.IsDevelopment() ? "Dev" : "AllowVite";
app.UseCors(corsPolicy);

// XSRF middleware (double submit). Placed *after* CORS and *before* auth so
// we can validate header+cookie early for state-changing endpoints.
app.UseMiddleware<ValidateXsrfMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
