using HahnSoftware.Domain.Exceptions;
using HahnSoftware.Domain.Interfaces.Services;

using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace HahnSoftware.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserIdentifier()
    {
        string? userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new AuthenticationException();
        }

        return Guid.Parse(userId);
    }
}
