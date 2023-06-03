using BuildingBlocks.Web;
using MealIngredients.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealIngredients.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
public sealed class MealIngredientController : BaseController 
{
    private readonly IMealIngredientsRepository _mealIngredientsRepository;
    public MealIngredientController(IMealIngredientsRepository mealIngredientsRepository)
    {
        _mealIngredientsRepository = mealIngredientsRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetMealIngredients()
    {
        try 
        {
            var results = await _mealIngredientsRepository.GetAllValues(AsNoTracking: true);
            
            return Ok(results);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {message = e.Message});
        }
    }
}