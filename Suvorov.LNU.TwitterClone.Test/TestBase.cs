using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Suvorov.LNU.TwitterClone.Core;
using Suvorov.LNU.TwitterClone.Database;

namespace Suvorov.LNU.TwitterClone.Test
{
    public class TestBase
{
    public IServiceProvider ServiceProvider { get; private set; }

    public ILogger Logger { get; private set; }

    protected TestBase()
    {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", false)
                        .AddJsonFile("appsettings.Development.json", true)
                        .AddUserSecrets<TestBase>();
=
        IConfigurationRoot configuration = builder.Build();

        var services = new ServiceCollection();

        services.AddLogging();
        services.RegisterCoreDependencies();
        services.RegisterCoreConfiguration(configuration);
        services.RegisterDatabseDependencies(configuration);

        ServiceProvider = services.BuildServiceProvider();
        Logger = ServiceProvider.GetRequiredService<ILogger<TestBase>>();
    }

    protected T ResolveService<T>() where T : class
    {
        return ServiceProvider.GetRequiredService<T>();
    }
}
}
