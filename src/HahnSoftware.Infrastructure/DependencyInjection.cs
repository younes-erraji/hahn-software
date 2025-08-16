using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Infrastructure;
using HahnSoftware.Infrastructure.Services;
using HahnSoftware.Infrastructure.Persistence;
using HahnSoftware.Infrastructure.Persistence.Repositories;
using HahnSoftware.Application.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void InfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterAuthentication(configuration);
        services.AddDbContext<HahnSoftwareDbContext>(options => options.UseSqlServer(configuration.GetConnection("SQL_Server")));

        // Repositories
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IPostBookmarkRepository, PostBookmarkRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Services
        services.AddSingleton<IMailService, MailService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
    }
}
