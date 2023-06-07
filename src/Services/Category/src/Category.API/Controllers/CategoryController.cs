using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BuildingBlocks.Services;
using BuildingBlocks.Web;
using MediatR;
using Category.Features.Queries.GetCategories;
using Category.Features.Queries.GetCategotyById;
using Category.Features.Commands.CreateCategory;
using Category.Features.Commands.DeleteCategory;
using BuildingBlocks.Commons.Exceptions;

namespace Category.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
public class CategoryController : BaseController
{
    public CategoryController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetCategories(CancellationToken cancellationToken)
    {
        try
        {
           var request = new GetCategoriesQuery();
           var results = await mediator.Send(request, cancellationToken);

           return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("{categoryId}", Name = "GetCategoryById")]
    public async Task<ActionResult> GetCategoryById(string categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetCategoryByIdQuery(categoryId);

            var result = await mediator.Send(request, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            if(ex is NotFoundException notFound)
                return NotFound(new {message = notFound.Message});
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory(CreateCategoryCommand createCategory, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(createCategory, cancellationToken);
            
            return CreatedAtRoute("GetCategoryById", new { categoryId = result }, result);
        }
        catch (Exception ex)
        {
            if(ex is ValidationException validation)
                return BadRequest(new {erorrs = validation.Errors});
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    // [HttpPatch("{categoryId}")]
    // public async Task<ActionResult> CreateCategory(string categoryId, JsonPatchDocument<UpdateCategoryDto> UpdateCategory, CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var category = await _categoryRepository.GetValue(x => x.Id.ToString() == categoryId, false);

    //         if (category == null)
    //         {
    //             return NotFound(new
    //             {
    //                 message = $"Category with Id '{categoryId}' was not found."
    //             });
    //         }

    //         var categoryToUpdate = new UpdateCategoryDto()
    //         {
    //             Name = category.Name,
    //         };

    //         UpdateCategory.ApplyTo(categoryToUpdate, (err) => BadRequest(new { message = err.ErrorMessage }));

    //         category.Name = categoryToUpdate.Name;

    //         await _categoryRepository.SaveChangesAsync(cancellationToken);

    //         return NoContent();
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
    //     }
    // }

    [HttpDelete("{categoryId}")]
    public async Task<ActionResult> DeleteCategory(string categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var request = new DeleteCategoryCommand(categoryId);

            await mediator.Send(request, cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            if(ex is NotFoundException notFound)
                return NotFound(new {message = notFound.Message});
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

}
