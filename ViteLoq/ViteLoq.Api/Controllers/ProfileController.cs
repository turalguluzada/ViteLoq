using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViteLoq.Infrastructure.Persistence;

namespace ViteLoq.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController : Controller
{
    private readonly ViteLoqDbContext _context;

    public ProfileController(ViteLoqDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userTotalSkin = await _context.UserTotalSkin.ToListAsync();
        return Ok(userTotalSkin);
    }
}