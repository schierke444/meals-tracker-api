using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.Events;
using BuildingBlocks.Services;
using BuildingBlocks.Web;
using MassTransit;
using Meals.Features.Meals.Commands.CreateMeal;
using Meals.Features.Meals.Commands.DeleteMealById;
using Meals.Features.Meals.Dtos;
using Meals.Features.Meals.Queries.GetMealById;
using Meals.Features.Meals.Queries.GetMealsByOwnerId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meals.API.Controllers;

[Route("Meals/v1/meals")]
[Authorize]
public class MealsController : BaseController 
{
    private readonly IRequestClient<CheckCategoryRecord> _categoryClient;
    private readonly IRequestClient<GetUserByIdRecord> _userClient;
    private readonly ICurrentUserService _currentUserService;

    public MealsController(IMediator mediator, IRequestClient<CheckCategoryRecord> categoryClient, IRequestClient<GetUserByIdRecord> userClient, ICurrentUserService currentUserService) : base(mediator)
    {
        _categoryClient = categoryClient;
        _userClient = userClient;
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
    public async Task<ActionResult<MealDetailsDto>> GetMealById(string mealId, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetMealByIdQuery(mealId);

            var result = await mediator.Send(request, cancellationToken);

            return Ok(result); 
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateMeals(CreateMealCommand createMeal, CancellationToken cancellationToken)
    {
        try
        {
            var categoryResponse = await _categoryClient 
                .GetResponse<CategoryRecordResult>(new CheckCategoryRecord { CategoryId = createMeal.CategoryId}, cancellationToken);

            var result = await mediator.Send(createMeal, cancellationToken);
            
            return CreatedAtRoute("GetMealById", new {mealId = result}, result);
        }
        catch(Exception ex)
        {
            if(ex is ValidationException validation)
                return BadRequest(new {errors = validation.Errors});
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
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
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

}
