using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Web;
using Category.Features.Commands.CreateCategory;
using Category.Features.Commands.DeleteCategory;
using Category.Features.Queries.GetCategories;
using Category.Features.Queries.GetCategotyById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Category.Features.Controllers;

[Route("[controller]/v1/category")]
[Authorize]
public class CategoryController : BaseController
{
    public CategoryController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetCategories(
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        try
        {
           var request = new GetCategoriesQuery(search, sortColumn, sortOrder, page, pageSize);
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
