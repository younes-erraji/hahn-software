using HahnSoftware.Domain.Entities;

namespace HahnSoftware.Domain.Interfaces.Services;

public interface IAuthenticationService
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken(Guid userId, bool RememberMe);
    string GenerateToken();
    string GetPassword(string key, string password);
}
