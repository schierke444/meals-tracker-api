using BuildingBlocks.Commons.Models.EventModels;
using BuildingBlocks.Events;
using BuildingBlocks.Services;
using BuildingBlocks.Web;
using MassTransit;
using Meals.API.Entities;
using Meals.API.Models;
using Meals.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meals.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
public class MealsController : BaseController
{
    private readonly IMealsRepository _mealsRepository;
    private readonly IRequestClient<CheckCategoryRecord> _categoryClient;
    private readonly IRequestClient<GetUserByIdRecord> _userClient;
    private readonly ICurrentUserService _currentUserService;
    private readonly IPublishEndpoint _publishEndpoint;
    public MealsController(IMealsRepository mealsRepository, IRequestClient<CheckCategoryRecord> categoryClient, ICurrentUserService currentUserService,IRequestClient<GetUserByIdRecord> userClient, IPublishEndpoint publishEndpoint)
    {
        _mealsRepository = mealsRepository; 
        _userClient = userClient;
        _categoryClient = categoryClient;
        _currentUserService = currentUserService;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
    {
        try
        {
            var results = await _mealsRepository.GetAllValues(true);

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
            var results = await _mealsRepository.GetValue(x => x.Id.ToString() == mealId);
            

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
            
            await _mealsRepository.Create(newMeal);
            await _mealsRepository.SaveChangesAsync(cancellationToken);
            
            // For Publishing an Event to MealIngredients.API 
            // To have a relationship with Recipes and Ingredients
            List<CreateMealEventDto> createMealsEvents = new();

            foreach (var mealItem in createMeals.Ingredients)
            {
                createMealsEvents.Add(new CreateMealEventDto(newMeal.Id, mealItem, newMeal.OwnerId));
            }

            await _publishEndpoint.Publish(new CreateMealEvent{MealIngredients = createMealsEvents}, cancellationToken);

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
            var results = await _mealsRepository.GetValue(x => x.Id.ToString() == mealId);
            if (results == null)
                return NotFound(new
                {
                    message = $"Meal with Id '{mealId}' was not found."
                });

            _mealsRepository.Delete(results);
            await _mealsRepository.SaveChangesAsync(cancellationToken);

            return NoContent(); 
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

}
