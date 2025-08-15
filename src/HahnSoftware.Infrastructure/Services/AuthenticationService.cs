using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Application.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HahnSoftware.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public AuthenticationService(IConfiguration configuration)
    {
        _issuer = configuration.GetConfig("Jwt:Issuer");
        _audience = configuration.GetConfig("Jwt:Audience");
        _secretKey = configuration.GetConfig("Jwt:SecretKey");
    }

    public string GenerateAccessToken(User user)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Mail)
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(bool RememberMe)
    {
        return new RefreshToken(Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)), RememberMe ? DateTimeOffset.Now.AddMonths(6) : DateTimeOffset.Now.AddDays(7));
    }

    public string GenerateToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

    public string GetPassword(string key, string password)
    {
        return $"#{key}#-#{password}#";
    }
}
