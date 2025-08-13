using Microsoft.Extensions.Configuration;

namespace HahnSoftware.Application.Extensions;

public static class ConfigurationExtension
{
    public static string GetConnection(this IConfiguration configuration, string name)
    {
        string? connectionString = configuration.GetConnectionString(name);
        return string.IsNullOrWhiteSpace(connectionString) ? throw new ArgumentNullException(name) : connectionString;
    }

    public static string GetConfig(this IConfiguration configuration, string name)
    {
        string? config = configuration[name];
        return string.IsNullOrWhiteSpace(config) ? throw new ArgumentNullException(name) : config;
    }
}
