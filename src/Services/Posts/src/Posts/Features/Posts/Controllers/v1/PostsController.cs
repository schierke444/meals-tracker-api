using Asp.Versioning;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.Web;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Posts.Features.Posts.Commands.CreatePost.v1;
using Posts.Features.Posts.Commands.DeletePostById;
using Posts.Features.Posts.Commands.UpdatePost.v1;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Queries.GetPostById.v1;
using Posts.Features.Posts.Queries.GetPosts.v1;
using Posts.Features.Posts.Queries.GetPostsByOwnerId.v1;

namespace Posts.Features.Posts.Controllers;

[ApiVersion(1.0)]
public class PostsController : BaseController
{
    public PostsController(IMediator mediator) : base(mediator)
    {
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<PaginatedResults<PostDetailsDto>>> GetPosts(
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            GetPostsQuery request = new(
                search,
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
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [HttpGet("users/{ownerId}/posts")]
    public async Task<ActionResult<PaginatedResults<PostsDto>>> GetPostsByOwnerId(
        string ownerId,
        string? sortColumn,
        string? sortOrder,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        try
        {
            GetPostsByOwnerIdQuery request = new(
                ownerId,
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
                NotFoundException notFound => BadRequest(new {message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [HttpGet("{postId}", Name = "GetPostById")]
    public async Task<ActionResult<PostDetailsDto>> GetPostById(string postId)
    {
        try
        {
            GetPostByIdQuery request = new(postId);

            var results = await mediator.Send(request);

            return Ok(results);
        }
        catch(Exception ex)
        {
            return ex switch {
                NotFoundException notFound => BadRequest(new {message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreatePost(CreatePostDto createPostDto)
    {
        try
        {
            CreatePostCommand createPost = new(createPostDto.Content);
            var id = await mediator.Send(createPost);

            return CreatedAtRoute("GetPostById", new { postId = id}, id);
        }
        catch(Exception ex)
        {
            return ex switch {
                ValidationException validation => BadRequest(new {errors = validation.Errors}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [Authorize]
    [HttpPatch("{postId}")]
    public async Task<ActionResult> UpdatePost(
        string postId,
        JsonPatchDocument<UpdatePostDto> UpdatePost,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            UpdatePostCommand request = new(postId, UpdatePost);
            var result = await mediator.Send(request, cancellationToken);

            return NoContent();
        }
        catch(Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [Authorize]
    [HttpDelete("{postId}")]
    public async Task<ActionResult> DeletePostById(string postId, CancellationToken cancellationToken = default)
    {
        try
        {
            DeletePostByIdCommand request = new(postId);
            var result = await mediator.Send(request, cancellationToken);

            return NoContent();
        }
        catch(Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }
}
