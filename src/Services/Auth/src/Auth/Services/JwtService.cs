﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Services;

public interface IJwtService
{
    string GenerateJwt(Guid Id, string Role, bool isRefreshToken);

    bool VerifyRefreshToken(string RefreshToken, out string userId);
}

public sealed class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    public JwtService(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateJwt(Guid Id, string Role, bool isRefreshToken)
    {
        var securityKey = new SymmetricSecurityKey
             (Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]!));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Id.ToString()),
            new Claim(ClaimTypes.Role, Role)
        };

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
        var decoded = new JwtSecurityTokenHandler().ValidateToken(
            RefreshToken,
            new TokenValidationParameters
            {
                ValidateActor = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Authentication:Issuer"],
                ValidAudience = _config["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"] ?? throw new ArgumentNullException()))
            },
            out SecurityToken validatedToken 
        );
        var jti = decoded.FindFirstValue(JwtRegisteredClaimNames.Jti);
        if (validatedToken is not JwtSecurityToken jwtSecurityToken ||
           !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase) ||
            jti is null)
        {
            userId = string.Empty;
            return false;
        }

        userId = jti; 
        return true;
    }
}
