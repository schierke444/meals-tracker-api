using Asp.Versioning;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Web;
using Comments.Features.Likes.Commands.LikeComment.v1;
using Comments.Features.Likes.Commands.UnlikeComment.v1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comments.Features.Likes.Controllers.v1;

[ApiVersion(1.0)]
[Authorize]
public class LikesController : BaseController
{
    public LikesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("{commentId}/like")]
    public async Task<IActionResult> LikeComment(string commentId, CancellationToken cancellationToken = default)
    {
        try
        {
            LikeCommentCommand request = new(commentId);

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

    [HttpPost("{commentId}/unlike")]
    public async Task<IActionResult> UnlikeComment(string commentId, CancellationToken cancellationToken = default)
    {
        try
        {
            UnlikeCommentCommand request = new(commentId);

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