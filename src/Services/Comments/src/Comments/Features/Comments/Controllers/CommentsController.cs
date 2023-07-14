using Asp.Versioning;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.Web;
using Comments.Features.Comments.Commands.AddComments.v1;
using Comments.Features.Comments.Commands.DeleteComments.v1;
using Comments.Features.Comments.Commands.UpdateComments.v1;
using Comments.Features.Comments.Dtos;
using Comments.Features.Comments.Queries.GetCommentById.v1;
using Comments.Features.Comments.Queries.GetCommentsByPostId.v1;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Comments.Features.Comments.Controllers;

[Authorize]
[ApiVersion(1.0)]
[Route("")]
public sealed class CommentsController : BaseController 
{
    public CommentsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("posts/{postId}/comments")]
    public async Task<ActionResult<PaginatedResults<CommentDetailsDto>>> GetCommentsByPostId(
        string postId,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            GetCommentsByPostIdQuery request = new(
                postId,
                sortColumn,
                sortOrder,
                page,
                pageSize
            );

            var results = await mediator.Send(request, cancellationToken);

            return Ok(results);
        }
        catch(Exception ex)
        {
            return ex switch {
                RequestFaultException requestFault => BadRequest(new {message = requestFault.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [HttpGet("{commentId}", Name = "GetCommentById")]
    public async Task<ActionResult<CommentDetailsDto>> GetCommentsById(
        string commentId,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            GetCommentByIdQuery request = new(commentId);

            var result = await mediator.Send(request, cancellationToken);

            return Ok(result);
        }
        catch(Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [HttpPost] 
    public async Task<ActionResult> AddComments(AddCommentsCommand addComments, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await mediator.Send(addComments, cancellationToken);

            return CreatedAtRoute("GetCommentById", new {commentId = result}, result);
        }      
        catch(Exception ex)
        {
            return ex switch {
                RequestFaultException requestFault => BadRequest(new {message = requestFault.Message}),
                ValidationException validation => BadRequest(new {errors = validation.Errors}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [HttpPatch("{commentId}")] 
    public async Task<ActionResult> UpdateComment(
        string commentId, 
        JsonPatchDocument<UpdateCommentDto> UpdateComment,
        CancellationToken cancellationToken = default)
    {
        try
        {
            UpdateCommentCommand request = new(commentId, UpdateComment);

            var result = await mediator.Send(request, cancellationToken);

            return NoContent(); 
        }      
        catch(Exception ex)
        {
            return ex switch {
                RequestFaultException requestFault => BadRequest(new {message = requestFault.Message}),
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                ConflictException conflict => BadRequest(new {message = conflict.Message}),
                ValidationException validation => BadRequest(new {errors = validation.Errors}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [HttpDelete("{commentId}")] 
    public async Task<ActionResult> DeleteComment(
        string commentId, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            DeleteCommentCommand request = new(commentId);

            var result = await mediator.Send(request, cancellationToken);

            return NoContent(); 
        }      
        catch(Exception ex)
        {
            return ex switch {
                RequestFaultException requestFault => BadRequest(new {message = requestFault.Message}),
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }
}