using BuildingBlocks.Events;
using BuildingBlocks.Web;
using MassTransit;
using Meals.API.Entities;
using Meals.API.Models;
using Meals.API.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meals.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
public class MealsController : BaseController
{
    private readonly IApplicationDbContext _context;
    private readonly IRequestClient<CheckCategoryRecord> _categoryClient;
    private readonly IRequestClient<GetUserByIdRecord> _userClient;
    private readonly ICurrentUserService _currentUserService;
    public MealsController(IApplicationDbContext context, IRequestClient<CheckCategoryRecord> categoryClient, ICurrentUserService currentUserService,IRequestClient<GetUserByIdRecord> userClient)
    {
        _context = context;
        _userClient = userClient;
        _categoryClient = categoryClient;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
    {
        try
        {
            var results = await _context.Meals.AsNoTracking().ToListAsync(); 

            return Ok(results); 
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("{mealId}", Name = "GetMealById")]
    public async Task<ActionResult<IEnumerable<Meal>>> GetMealById(string mealId)
    {
        try
        {
            var results = await _context.Meals
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.ToString() == mealId);

            if (results == null)
                return NotFound(new
                {
                    message = $"Meal with Id '{mealId}' was not found."
                });

            return Ok(results); 
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateMeals(CreateMealsDto createMeals, CancellationToken cancellationToken)
    {
        try
        {
            var categoryResponse = await _categoryClient 
                .GetResponse<CategoryRecordResult>(new CheckCategoryRecord { CategoryId = createMeals.CategoryId});

            var userResponse = await _userClient 
                .GetResponse<GetUserByIdResult>(new GetUserByIdRecord{ UserId = _currentUserService.UserId ?? string.Empty});

            Meal newMeal = new()
            {
                MealName = createMeals.MealName,
                MealReview = createMeals.MealReview,
                Rating = createMeals.Rating,
                CategoryId = Guid.Parse(createMeals.CategoryId),
                OwnerId = Guid.Parse(_currentUserService.UserId ?? throw new UnauthorizedAccessException())
            };
            
            await _context.Meals.AddAsync(newMeal);
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtRoute("GetMealById", new {mealId = newMeal.Id}, newMeal.Id);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpDelete("{mealId}")] 
    public async Task<ActionResult<IEnumerable<Meal>>> DeleteMealById(string mealId, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _context.Meals
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.ToString() == mealId);

            if (results == null)
                return NotFound(new
                {
                    message = $"Meal with Id '{mealId}' was not found."
                });

            _context.Meals.Remove(results);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent(); 
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

}
