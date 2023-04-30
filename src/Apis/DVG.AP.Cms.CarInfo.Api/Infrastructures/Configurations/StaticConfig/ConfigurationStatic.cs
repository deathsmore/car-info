using Dvg.AP.FE.CarInfo.Api.Infrastructures.Constants;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AutoPortal.Core.Exceptions;

namespace DVG.AP.Cms.CarInfo.Api.Infrastructures.Configurations.StaticConfig;

public static class ConfigurationStatic
{
    public static IConfiguration GetConfiguration(string? env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();


        // var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        try
        {
            // src/Apis/DVG.AP.Cms.NewCar.Api/Configurations/FileSettings/Testing/StorageSetting.json
            // Environment variable SITE_NAME set when build Docker images
            WebsiteManager.SiteName =
                Environment.GetEnvironmentVariable(
                    "SITE_NAME") ?? WebSiteDefaultConst.SITE_NAME
                ; //  Site name for get Configuration Files in Configurations/FileSettings 
            Console.WriteLine($"{DateTime.Now}: You're working on {WebsiteManager.SiteName}");
            var pathConfig =
                $"Infrastructures/Configurations/FileSettings/{WebsiteManager.SiteName}";

            var pathFileConfig = $"{pathConfig}/{env}/";
            Console.WriteLine($"{DateTime.Now}: You're working on {pathFileConfig}");
            foreach (var jsonFilename in Directory.EnumerateFiles(pathFileConfig,
                         "*.json", SearchOption.AllDirectories))
                builder.AddJsonFile(jsonFilename);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new NotFoundException(" Configuration file in environment:", env);
            // ignored
        }


        return builder.Build();
    }
}