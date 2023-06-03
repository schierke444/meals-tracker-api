using BuildingBlocks.Web;
using Ingredients.API.Entities;
using Ingredients.API.Models;
using Ingredients.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Ingredients.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
public class IngredientsController : BaseController
{
    private readonly IIngredientsRepository _ingredientsRepository;
    public IngredientsController(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetIngredients()
    {
        try
        {
            var results = await _ingredientsRepository.GetAllValues(
                x => new IngredientsDto { Id = x.Id, Name = x.Name},
                null,
                true
            );
            return Ok(results);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
    
    [HttpGet("{ingredientId}", Name = "GetIngredientById")]
    public async Task<ActionResult> GetIngredientById(string ingredientId)
    {
        try
        {
            var ingredient = await _ingredientsRepository.GetValue(
                x => x.Id.ToString() == ingredientId,
                x => new IngredientDetailsDto
                    { Id = x.Id, Name = x.Name, CreatedAt = x.CreatedAt, UpdatedAt = x.UpdatedAt }
            );

            if (ingredient == null)
                return NotFound(new { message = $"Ingredient with '{ingredientId}' was not found." });

            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddIngredient(AddIngredientDto addIngredient, CancellationToken cancellationToken = default)
    {
        try
        {
            var existingIngredient = await _ingredientsRepository.GetValue(
                x => x.Name.ToLower() == addIngredient.Name.ToLower()
            );

            if (existingIngredient != null)
                return BadRequest(new { message = $"Ingredient '{addIngredient.Name}' already exist." });

            Ingredient newIngredient = new()
            {
                Name = addIngredient.Name
            };

            await _ingredientsRepository.Create(newIngredient);
            await _ingredientsRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtRoute("GetIngredientById", new {ingredientId = newIngredient.Id}, newIngredient.Id);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpPatch("{ingredientId}")]
    public async Task<ActionResult> UpdateIngredient(string ingredientId,
        JsonPatchDocument<UpdateIngredientDto> updateIngredient,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var ingredient = await _ingredientsRepository.GetValue(
                x => x.Id.ToString() == ingredientId
            );

            if (ingredient == null)
                return NotFound(new { message = $"Ingredient with Id '{ingredientId}' was not found." });

            var ingredientToUpdate = new UpdateIngredientDto(ingredient.Name);

            updateIngredient.ApplyTo(ingredientToUpdate, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid JsonPatchDocument" });
            }

            var existingIngredient = await _ingredientsRepository.GetValue(
                x => x.Name.ToLower() == ingredientToUpdate.Name.ToLower()
            );

            if (existingIngredient != null)
                return BadRequest(new { message = $"Ingredient '{ingredientToUpdate.Name}' already exist." });

            ingredient.Name = ingredientToUpdate.Name;
            await _ingredientsRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }

    [HttpDelete("{ingredientId}")]
    public async Task<ActionResult> DeleteIngredient(string ingredientId, CancellationToken cancellationToken = default)
    {
        try
        {
            var existingIngredient = await _ingredientsRepository.GetValue(
                            x => x.Id.ToString() == ingredientId 
                        );
            
            if (existingIngredient == null) 
                return NotFound(new { message = $"Ingredient with Id '{ingredientId}' was not found." });

            _ingredientsRepository.Delete(existingIngredient);
            await _ingredientsRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
        }
    }
}