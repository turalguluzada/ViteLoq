using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViteLoq.Application.Interfaces.Templates;
using ViteLoq.Application.Interfaces.UserManagement;
using ViteLoq.Domain.Entities;
using ViteLoq.Infrastructure.Persistence;
using ViteLoq.Domain.Interfaces;
using ViteLoq.Infrastructure.Persistence;
using ViteLoq.Domain.Templates.Interfaces;
using ViteLoq.Infrastructure.Repositories.Templates;
using ViteLoq.Application.Services;
using ViteLoq.Application.Services.Templates;
using ViteLoq.Application.Services.UserManagement;
using ViteLoq.Domain.UserManagement.Interfaces;
using ViteLoq.Infrastructure.Repositories.UserManagement;


var builder = WebApplication.CreateBuilder(args);

// repositories

// builder.Services.AddScoped<IUserEntryRepository, EntryRepository>();
builder.Services.AddScoped<INutritionItemRepository, NutritionItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<INutritionItemService, NutritionItemService>();
builder.Services.AddScoped<IUserService, UserService>();
// UnitOfWork: resolve interface to concrete DbContext instance

builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ViteLoqDbContext>());

// application services
builder.Services.AddScoped<EntryService>();






// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();