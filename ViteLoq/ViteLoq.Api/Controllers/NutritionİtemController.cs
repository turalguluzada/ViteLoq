using Microsoft.AspNetCore.Mvc;
using ViteLoq.Application.DTOs.Templates.Nutrition;
using ViteLoq.Application.Interfaces.Templates;
using ViteLoq.Application.Services.Templates;
using ViteLoq.Domain.Templates.Entities;

namespace ViteLoq.Controllers;

[ApiController]
[Route("api/nutritionItem")]
public class NutritionİtemController : Controller
{
    private readonly INutritionItemService _nutritionItemService;

    public NutritionİtemController(INutritionItemService nutritionItemService)
    {
        _nutritionItemService = nutritionItemService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNutritionItemDto nutritionItemDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if(nutritionItemDto == null) 
            return StatusCode(404, new { errorCode = 1043, message = "NutritionItemDto is null" });
        
        var id = await _nutritionItemService.CreateAsync(nutritionItemDto);
        var nutritionItem = await _nutritionItemService.GetByIdAsync(id);
        return Ok(nutritionItem);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var nutritionItems = await _nutritionItemService.GetAllAsync();
        
        if(nutritionItems == null)
            return StatusCode(404, new { errorCode = 1042, message = "NutritionItems is null" });
        
        return Ok(nutritionItems);
    }
}