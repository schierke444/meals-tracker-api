using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuildingBlocks.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
}

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContext;
    public CurrentUserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }
    public string? UserId => _httpContext?.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Jti);
}
