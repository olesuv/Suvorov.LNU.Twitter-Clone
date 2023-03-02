using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suvorov.LNU.TwitterClone.Core.Interfaces;
using Suvorov.LNU.TwitterClone.Core.Services;
using Suvorov.LNU.TwitterClone.Models.Configuration;

namespace Suvorov.LNU.TwitterClone.Core
{
    public static class DIConfiguration
{
    public static void RegisterCoreDependencies(this IServiceCollection services)
    {
        services.AddTransient<IWeatherForecastService, WeatherForecastService>();
    }

    public static void RegisterCoreConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
    }
}
}
