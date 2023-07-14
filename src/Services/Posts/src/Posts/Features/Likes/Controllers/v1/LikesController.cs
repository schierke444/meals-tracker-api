using Asp.Versioning;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Web;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Posts.Features.Likes.Commands.LikePost.v1;
using Posts.Features.Likes.Commands.UnlikePost.v1;

namespace Posts.Features.Likes.Controllers.v1;

[ApiVersion(1.0)]
[Authorize]
public class LikesController : BaseController
{
    public LikesController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost("{postId}/like")]
    public async Task<IActionResult> LikePost(string postId, CancellationToken cancellationToken = default)
    {
        try
        {
            LikePostCommand request = new(postId);

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

    [HttpPost("{postId}/unlike")]
    public async Task<IActionResult> UnlikePost(string postId, CancellationToken cancellationToken = default)
    {
        try
        {
            UnlikePostCommand request = new(postId);

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