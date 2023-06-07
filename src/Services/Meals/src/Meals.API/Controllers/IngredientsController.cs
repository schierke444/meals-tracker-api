using BuildingBlocks.Web;
using Meals.Features.Ingredients.Commands.CreateIngredient;
using Meals.Features.Ingredients.Commands.DeleteIngredientById;
using Meals.Features.Ingredients.Queries.GetIngredientById;
using Meals.Features.Ingredients.Queries.GetIngredients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Ingredients.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
public class IngredientsController : BaseController 
{
    public IngredientsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetIngredients(CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetIngredientsQuery();
            var results = await mediator.Send(request, cancellationToken);
            return Ok(results);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
    
    [HttpGet("{ingredientId}", Name = "GetIngredientById")]
    public async Task<ActionResult> GetIngredientById(string ingredientId, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetIngredientByIdQuery(ingredientId);

            var result = await mediator.Send(request, cancellationToken);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateIngredient(CreateIngredientCommand createIngredient, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await mediator.Send(createIngredient, cancellationToken);

            return CreatedAtRoute("GetIngredientById", new {ingredientId = result}, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    // [HttpPatch("{ingredientId}")]
    // public async Task<ActionResult> UpdateIngredient(string ingredientId,
    //     JsonPatchDocument<UpdateIngredientDto> updateIngredient,
    //     CancellationToken cancellationToken = default)
    // {
    //     try
    //     {
    //         var ingredient = await _ingredientsRepository.GetValue(
    //             x => x.Id.ToString() == ingredientId
    //         );

    //         if (ingredient == null)
    //             return NotFound(new { message = $"Ingredient with Id '{ingredientId}' was not found." });

    //         var ingredientToUpdate = new UpdateIngredientDto(ingredient.Name);

    //         updateIngredient.ApplyTo(ingredientToUpdate, ModelState);

    //         if (!ModelState.IsValid)
    //         {
    //             return BadRequest(new { message = "Invalid JsonPatchDocument" });
    //         }

    //         var existingIngredient = await _ingredientsRepository.GetValue(
    //             x => x.Name.ToLower() == ingredientToUpdate.Name.ToLower()
    //         );

    //         if (existingIngredient != null)
    //             return BadRequest(new { message = $"Ingredient '{ingredientToUpdate.Name}' already exist." });

    //         ingredient.Name = ingredientToUpdate.Name;
    //         await _ingredientsRepository.SaveChangesAsync(cancellationToken);

    //         return NoContent();
    //     }
    //     catch (Exception e)
    //     {
    //         return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
    //     }
    // }

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