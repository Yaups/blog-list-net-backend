using blog_list_net_backend.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace blog_list_net_backend.Utils;

public static class AuthHelpers
{
    public static string GenerateJWTToken(UserWithoutBlogsDto user)
    {
        var claims = new List<Claim> {
        new Claim("id", user.Id.ToString()),
        new Claim("username", user.Username),
        new Claim("name", user.Name)
    };
        var jwtToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(ConfigValues.JWT_SECRET)
                    ),
                SecurityAlgorithms.HmacSha256Signature)
            );
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
