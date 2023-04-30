using DVG.AP.Cms.CarInfo.Api;
using DVG.AP.Cms.CarInfo.Api.Infrastructures.Configurations.StaticConfig;
using Microsoft.AspNetCore;
using NLog.Web;

try
{
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = ConfigurationStatic.GetConfiguration(env);
    var host = BuildWebHost(configuration, args);

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    await host.RunAsync();

    return 0;
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    return 1;
}
finally
{
}


IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((httpContext,x) => x.AddConfiguration(ConfigurationStatic.GetConfiguration(httpContext.HostingEnvironment.EnvironmentName)))
        .ConfigureKestrel((httpcontext, options) => { })
        .ConfigureLogging((_, logging) =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Information);
        })
        .UseNLog()
        .UseStartup<Startup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .Build();