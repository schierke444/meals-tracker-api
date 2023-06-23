using Auth.Commons.Dtos;
using Auth.Features.Commands.LoginUser;
using Auth.Features.Commands.LogoutUser;
using Auth.Features.Commands.RegisterUser;
using Auth.Features.Queries.RefreshUserToken;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Web;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Features.Controllers;

[Route("[controller]/v1")]
public class AuthController : BaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthDetailsDto>> LoginUser(LoginUserCommand loginUser, CancellationToken cancellationToken)
    {
        try
        {
            var (authDetails, refreshToken) = await mediator.Send(loginUser, cancellationToken);
            
            Response.Cookies.Append("rt", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(7)
            });
            return Ok(authDetails); 
            
        }
        catch(Exception ex)
        {
            if(ex is ValidationException validation)
                return BadRequest(new {errors = validation.Errors});
            if(ex is UnauthorizedAccessException unauthorized)
                return Unauthorized(new {message = unauthorized.Message});
            return StatusCode(StatusCodes.Status500InternalServerError,
            new { message = ex.Message,});
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthDetailsDto>> RegisterUser(RegisterUserCommand registerUser, CancellationToken cancellationToken)
    {
        try
        {
            var (authDetails, refreshToken) = await mediator.Send(registerUser, cancellationToken);

            Response.Cookies.Append("rt", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(7)
            });

            return Ok(authDetails); 
        }
        catch (Exception ex)
        {
            if(ex is ValidationException validation)
                return BadRequest(new {errors = validation.Errors});
            if(ex is ConflictException conflict)
                return BadRequest(new {message = conflict.Message});
            return StatusCode(StatusCodes.Status500InternalServerError,
            new { message = ex.Message,});
        }
    }

    [HttpGet("refresh")]
    public async Task<ActionResult<AuthDetailsDto>> RefreshUserToken(CancellationToken cancellationToken)
    {
        try
        {
            var cookie = Request.Cookies["rt"];
            if (cookie == null)
            {
                return Unauthorized("User Unauthorized.");
            }

            var request = new RefreshUserTokenQuery(cookie);

            var(authDetails, refreshToken) = await mediator.Send(request, cancellationToken);

            Response.Cookies.Append("rt", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(7)
            });

            return Ok(authDetails);
        }
        catch (Exception ex)
        {
            if(ex is UnauthorizedAccessException unauthorized)
                return BadRequest(new {message = unauthorized.Message});
            return StatusCode(StatusCodes.Status500InternalServerError,
            new { message = ex.Message,});
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogoutUser(CancellationToken cancellationToken)
    {
        try
        {
            LogoutUserCommand request = new();   
            await mediator.Send(request);
            Response.Cookies.Delete("rt", new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.Zero
            });
            return Ok(); 
        }
        catch(Exception ex)
        {
            if(ex is UnauthorizedAccessException unauthorized)
                return Unauthorized(new {message = unauthorized.Message});
            return StatusCode(StatusCodes.Status500InternalServerError,
            new { message = ex.Message,});
        }
    }
}
