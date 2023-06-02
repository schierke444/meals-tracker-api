using Auth.API.Entity;
using Auth.API.Models;
using Auth.API.Persistence;
using Auth.API.Services;
using BuildingBlocks.Services;
using BuildingBlocks.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Controllers;

[Route("api/v1/Auth")]
public class AuthController : BaseController
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;
    public AuthController(IApplicationDbContext context, IPasswordService passwordService, IJwtService jwtService)
    {
        _context = context;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(LoginUserDto loginUser)
    {
        try
        {
            var results = await _context.Users
                .Where(x => x.Username == loginUser.Username)
                .Select(u => new UserDetailsDto{ Id = u.Id, Username = u.Username, Password = u.Password})
                .FirstOrDefaultAsync();

            if (results == null || !_passwordService.VerifyPassword(results.Password, loginUser.Password))
                return Unauthorized(new { message = "Invalid Username or Password" });

            AuthDetailsDto authDetails = new()
            {
                Id = results.Id,
                Username = results.Username,
                AccessToken = _jwtService.GenerateJwt(results.Id, false)
            };

            Response.Cookies.Append("rt", _jwtService.GenerateJwt(results.Id, true), new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(7)
            });
            return Ok(authDetails); 
            
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    message = ex.Message,
                });
        }
    }

     [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterUserDto registerUser, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _context.Users
                .Where(x => x.Username.ToLower() == registerUser.Username.ToLower() || x.Email.ToLower() == registerUser.Email.ToLower())
                .Select(u => new UserDetailsDto { Id = u.Id, Username = u.Username, Password = u.Password })
                .FirstOrDefaultAsync();

            if (results != null)
                return BadRequest(new { message = "User already taken." });


            User newUser = new()
            {
                Username = registerUser.Username,
                Password = _passwordService.HashPassword(registerUser.Password),
                Email = registerUser.Email
            };

            await _context.Users.AddAsync(newUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            AuthDetailsDto authDetails = new()
            {
                Id = newUser.Id,
                Username = newUser.Username,
                AccessToken = _jwtService.GenerateJwt(newUser.Id, false)
            };

            Response.Cookies.Append("rt", _jwtService.GenerateJwt(newUser.Id, true), new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(7)
            });

            return Ok(authDetails); 
            
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    message = ex.Message,
                });
        }
    }

    [HttpGet("refresh")]
    public async Task<ActionResult<AuthDetailsDto>> RefreshUserToken()
    {
        try
        {
            var cookie = Request.Cookies["rt"];
            if(cookie == null || !_jwtService.VerifyRefreshToken(cookie, out string userId))
            {
                return Unauthorized("User Unauthorized.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == userId);
            if(user == null)
                return Unauthorized("User was not found.");

            AuthDetailsDto authDetails = new()
            {
                Id = user.Id,
                Username = user.Username,
                AccessToken = _jwtService.GenerateJwt(user.Id, false)
            };

            Response.Cookies.Append("rt", _jwtService.GenerateJwt(user.Id, true), new CookieOptions
            {
                HttpOnly = true,
                MaxAge = TimeSpan.FromDays(7)
            });
            return Ok(authDetails); 
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {message = ex.Message});
        }
    }
}
