using DVG.AP.Cms.CarInfo.Api;
using DVG.AP.Cms.CarInfo.Api.Infrastructures.Configurations.StaticConfig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NLog.Web;
using System.Net.Http;


namespace DVG.AP.CMS.CarInfo.API.IntergrationTests.Bases
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Program> where TStartup : class

    {
        protected override IHostBuilder? CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder =>
            {
                builder.UseEnvironment("Development");
                builder.ConfigureAppConfiguration(x => x.AddConfiguration(ConfigurationStatic.GetConfiguration("Development")))
                    .ConfigureLogging((_, logging) =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Information);
                    })
                    .UseNLog();
                builder.UseStartup<Startup>();
            }
            );
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
