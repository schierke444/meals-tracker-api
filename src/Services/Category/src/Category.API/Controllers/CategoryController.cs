using BuildingBlocks.Web;
using CategoryEntity = Category.API.Entities.Category;
using Category.API.Models;
using Category.API.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using BuildingBlocks.Services;

namespace Category.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
public class CategoryController : BaseController
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public CategoryController(ICurrentUserService currentUserService, IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetCategories()
    {
        try
        {
            var results = await _context.Categories.AsNoTracking().ToListAsync();

            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("{categoryId}", Name = "GetCategoryById")]
    public async Task<ActionResult> GetCategoryById(string categoryId)
    {
        try
        {
            var result = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToString() == categoryId);

            if (result == null)
                return NotFound(new
                {
                    message = $"Category with Id '{categoryId}' was not found."
                });

            return Ok(result);


        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory(AddCategoryDto addCategory, CancellationToken cancellationToken)
    {
        try
        {
            CategoryEntity newCategory = new()
            {
                Name = addCategory.Name
            };

            await _context.Categories.AddAsync(newCategory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtRoute("GetCategoryById", new { categoryId = newCategory.Id }, newCategory.Id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpPatch("{categoryId}")]
    public async Task<ActionResult> CreateCategory(string categoryId, JsonPatchDocument<UpdateCategoryDto> UpdateCategory, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id.ToString() == categoryId, cancellationToken: cancellationToken);

            if (category == null)
            {
                return NotFound(new
                {
                    message = $"Category with Id '{categoryId}' was not found."
                });
            }

            var categoryToUpdate = new UpdateCategoryDto()
            {
                Name = category.Name,
            };

            UpdateCategory.ApplyTo(categoryToUpdate, (err) => BadRequest(new { message = err.ErrorMessage }));

            category.Name = categoryToUpdate.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpDelete("{categoryId}")]
    public async Task<ActionResult> DeleteCategory(string categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToString() == categoryId);

            if (result == null)
                return NotFound(new
                {
                    message = $"Category with Id '{categoryId}' was not found."
                });

            _context.Categories.Remove(result);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

}
