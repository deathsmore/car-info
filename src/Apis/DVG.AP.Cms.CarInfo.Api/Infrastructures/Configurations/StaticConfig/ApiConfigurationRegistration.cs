using DVG.AP.Cms.CarInfo.Api.Infrastructure.Services;
using DVG.AP.Cms.CarInfo.Application;
using DVG.AP.Cms.CarInfo.GrpcPersistence;
using DVG.AP.Cms.CarInfo.Infrastructure.MiddlewareManager;
using DVG.AP.Cms.CarInfo.Persistence;

#pragma warning disable 1591

namespace DVG.AP.Cms.CarInfo.Api.Infrastructures.Configurations.StaticConfig
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiConfigurationRegistration
    {
        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServices(configuration);
            services.AddCarInfoPersistenceService(configuration);
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddPersistenceGrpcServiceRegistration(configuration);
        }

        public static void UserApiBuilder(this IApplicationBuilder app, IWebHostEnvironment env
        )
        {
            // app.AddSwaggerAppBuilder(env, apiVersionDescriptionProvider);
            // app.UseHttpsRedirection();
            // app.UseStaticFiles();
            app.UseCustomExceptionHandler();
            // app.Use(async (context, next) =>
            // {
            //     context.Request.EnableBuffering();
            //     await next();
            // });
        }
    }
}