using Asp.Versioning;
using Auth.Commons.Dtos;
using Auth.Features.Commands.LoginUser.v1;
using Auth.Features.Commands.LogoutUser.v1;
using Auth.Features.Commands.RegisterUser.v1;
using Auth.Features.Queries.RefreshUserToken;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Web;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Features.Controllers.v1;

[ApiVersion("1.0")]
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
                MaxAge = TimeSpan.FromDays(7),
                SameSite = SameSiteMode.Strict
            });
            return Ok(authDetails); 
            
        }
        catch(Exception ex)
        {
            return ex switch {
                ValidationException validation => BadRequest(new {message = validation.Message}),
                UnauthorizedAccessException unauthorized => BadRequest(new {message = unauthorized.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
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
                MaxAge = TimeSpan.FromDays(7),
                SameSite = SameSiteMode.Strict
            });

            return Ok(authDetails); 
        }
        catch (Exception ex)
        {
            return ex switch {
                ValidationException validation => BadRequest(new {message = validation.Message}),
                RequestFaultException requestFault => BadRequest(new {message = requestFault.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
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
                MaxAge = TimeSpan.FromDays(7),
                SameSite = SameSiteMode.Strict 
            });

            return Ok(authDetails);
        }
        catch (Exception ex)
        {
            return ex switch {
                UnauthorizedAccessException unauthorized => BadRequest(new {message = unauthorized.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }
    
    [Authorize]
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
                MaxAge = TimeSpan.Zero,
                SameSite = SameSiteMode.Strict
            });
            return Ok(); 
        }
        catch(Exception ex)
        {
            return ex switch {
                UnauthorizedAccessException unauthorized => BadRequest(new {message = unauthorized.Message}),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message})
            };
        }
    }
}
