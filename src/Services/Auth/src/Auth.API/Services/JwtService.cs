using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.API.Services;

public interface IJwtService
{
    string GenerateJwt(Guid Id, bool isRefreshToken);

    bool VerifyRefreshToken(string RefreshToken, out string userId);
}

public sealed class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    public JwtService(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateJwt(Guid Id, bool isRefreshToken)
    {
        var securityKey = new SymmetricSecurityKey
             (Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]!));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Id.ToString())        };

        var tokenToWrite = new JwtSecurityToken
            (
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claims,
                DateTime.Now,
                isRefreshToken ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(12),
                signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(tokenToWrite);
    }

    public bool VerifyRefreshToken(string RefreshToken, out string userId)
    {
        throw new NotImplementedException();
    }
}
