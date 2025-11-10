
using MyAdvancedApi.Services;

namespace MyAdvancedApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // --- Add ALL your services here ---
        services.AddScoped<UserService>();
        services.AddScoped<TokenService>();

        return services;
    }
}