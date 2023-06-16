using BuildingBlocks.Services;
using BuildingBlocks.Web;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Posts.Features.Posts.Commands.CreatePost;
using Posts.Features.Posts.Commands.DeletePostById;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Queries.GetAllPostsByOwnerId;
using Posts.Features.Posts.Queries.GetPostById;

namespace Posts.Features.Posts.Controllers;

[ApiController]
[Route("[controller]/v1/posts")]
[Authorize]
public class PostsController : BaseController
{
    private readonly ICurrentUserService _currentUserService;
    public PostsController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator)
    {
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostsDto>>> GetPosts(int page = 1, int pageSize = 10)
    {
        try
        {
            var userId = _currentUserService.UserId;
            if(userId is null)
                return Unauthorized();

            GetAllPostsByOwnerIdQuery request = new(page, pageSize, userId);

            var results = await mediator.Send(request);

            return Ok(results);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message});
        }
    }

    [HttpGet("{postId}", Name = "GetPostById")]
    public async Task<ActionResult<PostDetailsDto>> GetPostById(string postId)
    {
        try
        {
            var userId = _currentUserService.UserId;
            if(userId == null)
                return Unauthorized();

            GetPostByIdQuery request = new(postId, userId);

            var results = await mediator.Send(request);

            return Ok(results);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message});
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreatePost(CreatePostDto createPostDto)
    {
        try
        {
            var userId = _currentUserService.UserId;
            if(userId == null)
                return Unauthorized();
            
            CreatePostCommand createPost = new(createPostDto.Content, Guid.Parse(userId));
            var id = await mediator.Send(createPost);

            return CreatedAtRoute("GetPostById", new { postId = id}, id);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message});
        }
    }

    [HttpDelete("{postId}")]
    public async Task<ActionResult> DeletePostById(string postId)
    {
        try
        {
            var userId = _currentUserService.UserId;
            if(userId == null)
                return Unauthorized();

            DeletePostByIdCommand request = new(postId, userId);
            var result = await mediator.Send(request);

            return NoContent();
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message});
        }
    }
}
