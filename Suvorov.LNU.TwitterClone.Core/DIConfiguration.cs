using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suvorov.LNU.TwitterClone.Core.Interfaces;
using Suvorov.LNU.TwitterClone.Core.Mapper;
using Suvorov.LNU.TwitterClone.Models.Configuration;

namespace Suvorov.LNU.TwitterClone.Core
{
    public static class DIConfiguration
{
    public static void RegisterCoreDependencies(this IServiceCollection services)
    {
            services.AddSingleton<IMapperProvider, MapperProvider>();
            services.AddSingleton(GetMapper);
    }

    private static IMapper GetMapper(IServiceProvider serviceProvider)
    {
        var provider = serviceProvider.GetRequiredService<IMapperProvider>();
        return provider.GetMapper();
    }

    public static void RegisterCoreConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
    }
}
}
