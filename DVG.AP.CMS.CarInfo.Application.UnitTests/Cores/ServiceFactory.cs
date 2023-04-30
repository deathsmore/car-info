using DVG.AP.Cms.CarInfo.Application;
using DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Cores
{
    public static class ServiceFactory
    {
        public static ServiceProvider SetupConfiguration()
        {
            IConfiguration configuration = ConfigurationStatic.GetConfiguration("Development");
            IServiceCollection services = new ServiceCollection();

            services.AddHttpContextAccessor();
            //services.AddPersistenceServices(configuration);
            services.AddApplicationServices();
            services.AddScoped<ILoggerFactory, LoggerFactory>();

            //services.AddScoped<IRequestHandlerBase<ClientPromotionDetailDto, ClientPromotionDetailQuery>, ClientPromotionDetailQueryHandler>();

            var provider = services.BuildServiceProvider();
            return provider;
        }
    }
}
