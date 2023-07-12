using Asp.Versioning;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.Services;
using BuildingBlocks.Web;
using Meals.Features.Meals.Commands.CreateMeal.v1;
using Meals.Features.Meals.Commands.DeleteMealById.v1;
using Meals.Features.Meals.Commands.UpdateMeal.v1;
using Meals.Features.Meals.Dtos;
using Meals.Features.Meals.Queries.GetMealById.v1;
using Meals.Features.Meals.Queries.GetMealsByOwnerId.v1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meals.Features.Meals.Controllers.v1;

[ApiVersion(1.0)]
[Authorize]
public class MealsController : BaseController 
{
    private readonly ICurrentUserService _currentUserService;
    public MealsController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator)
    {
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResults<MealDetailsDto>>> GetMeals(
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1, 
        int pageSize = 10, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var userId = _currentUserService.UserId;
            if(userId is null)
                return Unauthorized();

            var request = new GetMealsByOwnerIdQuery(
                userId, 
                search,
                sortColumn,
                sortOrder,
                page, 
                pageSize);

            var results = await mediator.Send(request, cancellationToken);

            return Ok(results); 
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("{mealId}", Name = "GetMealById")]
    public async Task<ActionResult<MealDetailsDto>> GetMealById(
        string mealId, 
        [FromQuery]bool includeIngredients = true,
        [FromQuery]bool includeCategory = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new GetMealByIdQuery(mealId, includeIngredients, includeCategory);

            var result = await mediator.Send(request, cancellationToken);

            return Ok(result); 
        }
        catch(Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new { message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message })
            }; 
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateMeals(CreateMealCommand createMeal, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(createMeal, cancellationToken);
            
            return CreatedAtRoute("GetMealById", new {mealId = result}, result);
        }
        catch(Exception ex)
        {
            return ex switch {
                ValidationException validation => BadRequest(new {errors = validation.Errors}),
                ConflictException conflict => BadRequest(new {message = conflict.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }


    [HttpPut("{mealId}")] 
    public async Task<ActionResult> DeleteMealById(string mealId, UpdateMealDto updateMeal, CancellationToken cancellationToken)
    {
        try
        {
            UpdateMealCommand request = new(mealId, updateMeal);

            await mediator.Send(request, cancellationToken);

            return NoContent(); 
        }
        catch(Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                ValidationException validation => BadRequest(new {errors = validation.Errors}),
                ConflictException conflict => BadRequest(new {message = conflict.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [HttpDelete("{mealId}")] 
    public async Task<ActionResult> DeleteMealById(string mealId, CancellationToken cancellationToken)
    {
        try
        {
            var request = new DeleteMealByIdCommand(mealId);

            await mediator.Send(request, cancellationToken);

            return NoContent(); 
        }
        catch(Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

}
