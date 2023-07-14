using Asp.Versioning;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Web;
using Meals.Features.Likes.Commands.LikeMeal.v1;
using Meals.Features.Likes.Commands.UnlikeMeal.v1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Posts.Features.Likes.Controllers.v1;

[ApiVersion(1.0)]
[Authorize]
public class LikesController : BaseController
{
    public LikesController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost("{mealId}/like")]
    public async Task<IActionResult> LikeMeal(string mealId, CancellationToken cancellationToken = default)
    {
        try
        {
            LikeMealCommand request = new(mealId);

            await mediator.Send(request, cancellationToken);

            return Ok();
        } 
        catch(Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                ConflictException conflict => NotFound(new {message = conflict.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [HttpPost("{mealId}/unlike")]
    public async Task<IActionResult> UnlikeMeal(string mealId, CancellationToken cancellationToken = default)
    {
        try
        {
            UnlikeMealCommand request = new(mealId);

            await mediator.Send(request, cancellationToken);

            return Ok();
        } 
        catch(Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                ConflictException conflict => NotFound(new {message = conflict.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }
}