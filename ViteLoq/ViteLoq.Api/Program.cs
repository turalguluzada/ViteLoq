using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViteLoq.Application.Interfaces.Templates;
using ViteLoq.Application.Interfaces.UserManagement;
using ViteLoq.Domain.Entities;
using ViteLoq.Infrastructure.Persistence;
using ViteLoq.Domain.Interfaces;
using ViteLoq.Domain.Templates.Interfaces;
using ViteLoq.Infrastructure.Repositories.Templates;
using ViteLoq.Application.Services;
using ViteLoq.Application.Services.Templates;
using ViteLoq.Application.Services.UserManagement;
using ViteLoq.Domain.UserManagement.Interfaces;
using ViteLoq.Infrastructure.Repositories.UserManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ViteLoq.Application.Interfaces.Auth;
using ViteLoq.Application.Services.Auth;
using ViteLoq.Domain.Token.Interfaces;
using ViteLoq.Infrastructure.Repositories.Auth;


var builder = WebApplication.CreateBuilder(args);
//#region JwtValues
var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];
//#endregion

//#region JwtForCookie?
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };

        // IMPORTANT: read token from cookie if not present in Authorization header
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // prefer Authorization header
                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader)) return Task.CompletedTask;

                // if no header, try cookie
                if (context.Request.Cookies.TryGetValue("access_token", out var tokenFromCookie))
                {
                    context.Token = tokenFromCookie;
                }
                return Task.CompletedTask;
            }
        };
    });
// CORS: allow credentials and your frontend origin(s)
builder.Services.AddCors(options =>
{
    options.AddPolicy("spa", policy =>
    {
        policy
            .WithOrigins("https://localhost:7114") // <<< frontend origin EXACT; include https if using TLS
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddAuthorization();
//#endregion




builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<Program>();
    });

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

// builder.Services.AddScoped<IUserEntryRepository, EntryRepository>();
builder.Services.AddScoped<INutritionItemRepository, NutritionItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<INutritionItemService, NutritionItemService>();
builder.Services.AddScoped<IUserService, UserService>();
// UnitOfWork: resolve interface to concrete DbContext instance

builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ViteLoqDbContext>());

// application services
builder.Services.AddScoped<EntryService>();

// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = true;
//         options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidIssuer = issuer,
//             ValidateAudience = true,
//             ValidAudience = audience,
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
//             ValidateLifetime = true,
//             ClockSkew = TimeSpan.FromSeconds(30)
//         };
//     });

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// EF & Identity
builder.Services.AddDbContext<ViteLoqDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ViteLoqDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
});

// Controllers
builder.Services.AddControllers();

// !!!!! DEV ONLY: Allow all CORS for quick testing
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevAllowAll", p => p
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Enable CORS (DevAllowAll)
app.UseCors("DevAllowAll");



app.UseCors("spa"); // əvvəlcə policy əlavə etdiyini farz edirik
// Static files (demo HTML) — optional
app.UseStaticFiles();
// XSRF double-submit middleware
app.UseMiddleware<ViteLoq.Api.Middlewares.ValidateXsrfMiddleware>();
// Authentication & Authorization



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();