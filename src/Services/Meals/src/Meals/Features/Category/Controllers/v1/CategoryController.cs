using Asp.Versioning;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Web;
using Category.Features.Dtos;
using Meals.Features.Category.Commands.CreateCategory.v1;
using Meals.Features.Category.Commands.DeleteCategory.v1;
using Meals.Features.Category.Commands.UpdateCategory.v1;
using Meals.Features.Category.Queries.GetCategories.v1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace Meals.Features.Category.Controllers.v1;

[ApiVersion(1.0)]
[Route("category")]
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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> CreateCategory(CreateCategoryCommand createCategory, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(createCategory, cancellationToken);
            
            return StatusCode(StatusCodes.Status201Created, new {id = result});
        }
        catch (Exception ex)
        {
            return ex switch {
                ValidationException validation => BadRequest(new {errors = validation.Errors}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{categoryId}")]
    public async Task<ActionResult> UpdateCategory(string categoryId, JsonPatchDocument<UpdateCategoryDto> UpdateCategory, CancellationToken cancellationToken)
    {
        try
        {
            UpdateCategoryCommand request = new(categoryId, UpdateCategory);

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
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

}
