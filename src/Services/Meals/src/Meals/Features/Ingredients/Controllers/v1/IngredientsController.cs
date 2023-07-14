using Asp.Versioning;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Web;
using Meals.Features.Ingredients.Commands.CreateIngredient.v1;
using Meals.Features.Ingredients.Commands.DeleteIngredientById.v1;
using Meals.Features.Ingredients.Commands.UpdateIngredient.v1;
using Meals.Features.Ingredients.Dtos;
using Meals.Features.Ingredients.Queries.GetIngredients;
using Meals.Features.Ingredients.Queries.GetIngredientsByMealId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Ingredients.API.Controllers;

[ApiVersion(1.0)]
[Route("ingredients")]
public class IngredientsController : BaseController 
{
    public IngredientsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetIngredients(
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new GetIngredientsQuery(search, sortColumn, sortOrder, page, pageSize);
            var results = await mediator.Send(request, cancellationToken);
            return Ok(results);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpGet("{mealId}/list", Name = "GetIngredientsByMealId")]
    public async Task<ActionResult> GetIngredientsByMealId(
        string mealId, 
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new GetIngredientsByMealIdQuery(
                mealId,
                search,
                sortColumn,
                sortOrder,
                page,
                pageSize
            );

            var result = await mediator.Send(request, cancellationToken);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> CreateIngredient(CreateIngredientCommand createIngredient, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await mediator.Send(createIngredient, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, new {id = result});
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpPatch("{ingredientId}")]
    public async Task<ActionResult> UpdateIngredient(
        string ingredientId,
        JsonPatchDocument<UpdateIngredientDto> updateIngredient,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            UpdateIngredientCommand request = new(ingredientId, updateIngredient);

            await mediator.Send(request, cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            return ex switch {
                ValidationException validation => BadRequest(new {errors = validation.Errors}),
                ConflictException conflict => BadRequest(new {message = conflict.Message}),
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{ingredientId}")]
    public async Task<ActionResult> DeleteIngredient(string ingredientId, CancellationToken cancellationToken)
    {
        try
        {
            var request = new DeleteIngredientByIdCommand(ingredientId);

            await mediator.Send(request, cancellationToken);

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
}