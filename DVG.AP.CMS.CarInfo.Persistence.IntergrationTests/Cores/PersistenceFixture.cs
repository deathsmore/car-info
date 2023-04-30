using System;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.CMS.CarInfo.Persistence.IntergrationTests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DVG.AP.CMS.CarInfo.Persistence.IntergrationTests.Cores
{
    public class PersistenceFixture : IDisposable
    {
        public IBrandRepository? BrandRepository { get; private set; }

        public PersistenceFixture()
        {
            var services = new ServiceCollection();
            services.AddScoped<ILoggerFactory, LoggerFactory>();

            var provider = services.BuildServiceProvider();
            BrandRepository = provider.GetService<IBrandRepository>();
        }

        public void Dispose()
        {
        }
    }
}
