using System.Reflection;
using DVG.AP.Cms.CarInfo.Api.Infrastructures.Constants;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Create;
using DVG.AutoPortal.Core.Validations;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

namespace DVG.AP.Cms.CarInfo.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddVersionedApiExplorer(setupAction => { setupAction.GroupNameFormat = "'v'VV"; });

        services.AddApiVersioning(setupAction =>
        {
            setupAction.AssumeDefaultVersionWhenUnspecified = true;
            setupAction.DefaultApiVersion = new ApiVersion(1, 0);
            setupAction.ReportApiVersions = true;
            //setupAction.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //setupAction.ApiVersionReader = new MediaTypeApiVersionReader();
        });

        var apiVersionDescriptionProvider =
            services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

        services.AddSwaggerGen(setupAction =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                setupAction.SwaggerDoc(
                    $"{ApiConst.SwaggerDocName}{description.GroupName}",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Dvg.AP.Cms.CarInfo API",
                        Version = description.ApiVersion.ToString(),
                        Description = "Through this API you can access DVG AutoPortal Cms CarInfo Api.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "sondt@daivietgroup.net.vn",
                            Name = "Dang Thai Son",
                            Url = new Uri("https://www.twitter.com/sondt")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });
            }


            setupAction.DocInclusionPredicate((documentName, apiDescription) =>
            {
                var actionApiVersionModel = apiDescription.ActionDescriptor
                    .GetApiVersionModel(ApiVersionMapping.Explicit | ApiVersionMapping.Implicit);

                if (actionApiVersionModel.DeclaredApiVersions.Any())
                {
                    return actionApiVersionModel.DeclaredApiVersions.Any(v =>
                        $"{ApiConst.SwaggerDocName}v{v.ToString()}" == documentName);
                }

                return actionApiVersionModel.ImplementedApiVersions.Any(v =>
                    $"{ApiConst.SwaggerDocName}v{v.ToString()}" == documentName);
            });


            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            setupAction.IncludeXmlComments(xmlCommentsFullPath);
            // setupAction.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
        });
    }

    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        services.AddControllersWithViews(options =>
            {
                options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
                options.Filters.Add(typeof(CustomValidationModel));
            })
            .AddJsonOptions(
                options =>
                {
                    // options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                }
            );


        services.AddFluentValidation(c =>
        {
            c.RegisterValidatorsFromAssemblyContaining<CreateCarInfoPropertyValueValidator>();
        });
        services.AddFluentValidationRulesToSwagger();
        return services;
    }

    private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
    {
        var builder = new ServiceCollection()
            .AddLogging()
            .AddMvc()
            .AddNewtonsoftJson()
            .Services.BuildServiceProvider();

        return builder
            .GetRequiredService<IOptions<MvcOptions>>()
            .Value
            .InputFormatters
            .OfType<NewtonsoftJsonPatchInputFormatter>()
            .First();
    }
}