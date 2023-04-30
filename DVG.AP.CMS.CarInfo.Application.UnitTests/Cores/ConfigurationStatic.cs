using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AutoPortal.Core.Exceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Cores
{
    public class ConfigurationStatic
    {
        public static IConfiguration GetConfiguration(string? env = null)
        {
            env = env != null ? env : Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();


            try
            {
                // src/Apis/DVG.AP.Cms.NewCar.Api/Configurations/FileSettings/Testing/StorageSetting.json
                // Environment variable SITE_NAME set when build Docker images
                WebsiteManager.SiteName =
                    Environment.GetEnvironmentVariable(
                        "SITE_NAME") ?? "Philkotse"
                    ; //  Site name for get Configuration Files in Configurations/FileSettings 
                Console.WriteLine($"You're working on {WebsiteManager.SiteName}");
                var pathConfig =
                    $"Infrastructures/Configurations/FileSettings/{WebsiteManager.SiteName}";

                var pathFileConfig = $"{pathConfig}/{env}/";
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
}
