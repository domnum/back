using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Domain.Entities.Core;
using Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;


public sealed class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        var tokenhandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(user.GetClaims()),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        return tokenhandler.WriteToken(tokenhandler.CreateToken(tokenDescriptor));
    }
}

public static class RoleClaimsExtensions
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {   
        var result = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email.Address!),
        };

        if (user.StudentId is not null)
        {
            result.Add(new Claim("StudentId", user.StudentId.ToString()!));
        }
        return result;
    }
}
