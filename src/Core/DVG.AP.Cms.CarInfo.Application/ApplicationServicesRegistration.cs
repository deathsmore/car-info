using System.Reflection;
using DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices;
using DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices.Interfaces;
using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ServiceLocators;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DVG.AP.Cms.CarInfo.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        StaticVariables.AMPSettings = configuration.GetSection("AMPSetting").Get<AMPSetting>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped<NewCarServiceLocator>();
        services.AddScoped<NewCarArticleFactory>();
        services.AddScoped<NewCarArticleModel>();
        services.AddScoped<NewCarArticleBrand>();
        services.AddScoped<NewCarArticleVariant>();
        services.AddScoped<NewCarArticleDefault>();
        services.AddTransient<IModelPropertySummariesService, ModelPropertySummariesService>();
        services.AddTransient<IModelPropertyValuesService, ModelPropertyValuesService>();
        services.AddTransient<INewCarVariantService, NewCarVariantService>();
        return services;
    }
}