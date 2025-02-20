using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infraestructure.Security.Tokens.Access;

public static class JwtTokenGenerator
{
    public static string Generate(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = SecurityKey();
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = credentials,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public static SymmetricSecurityKey SecurityKey()
    {
        var symmetricKey = Encoding.UTF8.GetBytes(ApiConfiguration.JwtPrivateKey);
        return new SymmetricSecurityKey(symmetricKey);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Name, user.Name)
        };

        return new ClaimsIdentity(claims);
    }
}
