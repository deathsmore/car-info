using DVG.AP.Cms.CarInfo.Api.Extensions;
using DVG.AP.Cms.CarInfo.Api.Infrastructures.Configurations.StaticConfig;
using DVG.AP.Cms.CarInfo.Api.Infrastructures.Constants;
using DVG.AutoPortal.Logging.Extensions;
using DVG.AutoPortal.Logging.StaticConfig;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.IdentityModel.Tokens;

#pragma warning disable CS1591

namespace DVG.AP.Cms.CarInfo.Api;

public class Startup
{
    readonly string CorsPolicyOrigins = "CmsCarInfo";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;

        var configFilePath = configuration.GetSection("Logging:Providers:NLog:ConfigFilePath").Get<string>();
        var projectLogConfig =
            configuration.GetSection("Logging:ProjectLogConfig").Get<ProjectLogConfig>();

        LoggingBuilderExten.UseNLog(configFilePath, configuration.GetSection("Logging:KafkaTaget").Get<string>(),
            projectLogConfig, new LoggerFactory());
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    private IApiVersionDescriptionProvider? apiVersionDescriptionProvider;

    public void ConfigureServices(IServiceCollection services)
    {
        var corsAllowDomain = Configuration.GetSection("AllowDomain").Get<string[]>();

        services
            .AddCustomMvc()
            .AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicyOrigins,
                    builder => builder.WithOrigins(corsAllowDomain).AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .SetIsOriginAllowed((host) => true));
            })
            .AddCustomSwagger(Configuration);

        //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
        //    .AddIdentityServerAuthentication(options =>
        //    {
        //        options.Authority = Configuration.GetSection("Authentication:IdentityServerDomain").Get<string>();
        //        options.ApiName = "DVG.AutoPortal.CMS.CarInfo";
        //    });
        Console.WriteLine(
            $"-----------> Authority-----{Configuration.GetSection("Authentication:IdentityServerDomain").Get<string>()}");
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = Configuration.GetSection("Authentication:IdentityServerDomain").Get<string>();
                options.Audience = "DVG.AutoPortal.CMS.CarInfo";
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                //options.RequireHttpsMetadata = false;
                //options.TokenValidationParameters = new TokenValidationParameters()
                //{
                //    ValidateIssuer = true,
                //    ValidateAudience = true,
                //    ValidAudience = configuration["JWT:ValidAudience"],
                //    ValidIssuer = configuration["JWT:ValidIssuer"],
                //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                //};
            });

        services.AddApiServices(Configuration);

        apiVersionDescriptionProvider =
            services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    setupAction.SwaggerEndpoint($"/swagger/" +
                                                $"{ApiConst.SwaggerDocName}{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                // setupAction.RoutePrefix = string.Empty;
            });
        }


        app.UserApiBuilder(env);
        app.UseRouting();
        app.UseCors(CorsPolicyOrigins);
        // app.EnableCors();
        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireCors(CorsPolicyOrigins); });
    }
}