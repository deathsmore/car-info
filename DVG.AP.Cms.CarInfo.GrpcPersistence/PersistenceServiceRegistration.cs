using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Common;
using DVG.AP.Cms.CarInfo.GrpcPersistence.Config;
using DVG.AP.Cms.CarInfo.GrpcPersistence.Infrastructures;
using DVG.AP.Cms.CarInfo.GrpcPersistence.Repositories.Common;
using DVG.AP.Cms.Common.Api.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.GrpcPersistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceGrpcServiceRegistration(this IServiceCollection services,
           IConfiguration configuration)
        {
            GrpcPersistenceSetting.GrpcSetting = configuration.GetSection("GrpcSetting").Get<GrpcSetting>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<GrpcExceptionInterceptor>();

            services.AddGrpcClient<UserGrpc.UserGrpcClient>((servicesProvider, options) =>
            {
                options.Address = new Uri(GrpcPersistenceSetting.GrpcSetting.Urls.GrpcCommon!);
            }).AddInterceptor<GrpcExceptionInterceptor>();

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
