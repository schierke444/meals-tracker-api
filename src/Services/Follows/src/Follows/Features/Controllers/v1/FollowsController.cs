using Asp.Versioning;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.Web;
using Follows.Features.Commands.FollowUser.v1;
using Follows.Features.Commands.UnfollowUser.v1;
using Follows.Features.Dtos;
using Follows.Features.Queries.GetUsersFollowers.v1;
using Follows.Features.Queries.GetUsersFollowersAndFollowingsCount.v1;
using Follows.Features.Queries.GetUsersFollowings.v1;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Follows.Features.Controllers.v1;

[ApiVersion(1.0)]
[Route("")]
public class FollowsController : BaseController
{
    public FollowsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{userId}/followers")]
    public async Task<ActionResult<PaginatedResults<UserDto>>> GetUsersFollowers(
        string userId, 
        string? Search,
        string? SortColumn,
        string? SortOrder,
        int page = 1, 
        int pageSize = 10,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            GetUsersFollowersQuery request = new(
                userId,
                Search,
                SortColumn,
                SortOrder,
                page,
                pageSize
            );

            var results = await mediator.Send(request, cancellationToken);

            return Ok(results);
        }
        catch (Exception ex)
        {
            return ex switch {
                RequestFaultException requestFault => BadRequest(new {message = requestFault.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [HttpGet("{userId}/followings")]
    public async Task<ActionResult<PaginatedResults<UserDto>>> GetUsersFollowings(
        string userId, 
        string? Search,
        string? SortColumn,
        string? SortOrder,
        int page = 1, 
        int pageSize = 10,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            GetUsersFollowingsQuery request = new(
                userId,
                Search,
                SortColumn,
                SortOrder,
                page,
                pageSize
            );


            var results = await mediator.Send(request, cancellationToken);

            return Ok(results);
        }
        catch (Exception ex)
        {
            return ex switch {
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [HttpGet("{userId}/ffcount")]
    public async Task<ActionResult<FollowersAndFollowingsDto>> GetUsersFollowersAndFollowingsCount(
        string userId,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            GetUsersFollowersAndFollowingsCountQuery request = new(userId);

            var results = await mediator.Send(request);

            return Ok(results);
        }
        catch (Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [Authorize]
    [HttpPost("{userId}/follow")]
    public async Task<ActionResult> FollowUser(
        string userId,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            FollowUserCommand request = new(userId);
            await mediator.Send(request);

            return Ok();
        }
        catch (Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                ConflictException conflict => BadRequest(new {message = conflict.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }

    [Authorize]
    [HttpPost("{userId}/unfollow")]
    public async Task<ActionResult> UnfollowUser(
        string userId, 
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            UnfollowUserCommand request = new(userId);
            await mediator.Send(request, cancellationToken);

            return Ok();
        }
        catch (Exception ex)
        {
            return ex switch {
                NotFoundException notFound => NotFound(new {message = notFound.Message}),
                ConflictException conflict => BadRequest(new {message = conflict.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }
}