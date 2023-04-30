using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DVG.AP.CMS.CarInfo.TestBase.Fake;
public static class SetupFactory
{
    public static ServiceProvider SetupConfiguration(IServiceCollection services)
    {
        services.AddScoped<Microsoft.Extensions.Logging.ILoggerFactory, LoggerFactory>();
        var provider = services.BuildServiceProvider();
        return provider;
    }

    public static IConfiguration CreateConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json",
                         false,
                         true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}