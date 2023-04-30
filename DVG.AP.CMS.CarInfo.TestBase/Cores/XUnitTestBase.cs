using DVG.AP.CMS.CarInfo.TestBase.Fake;
using Microsoft.Extensions.DependencyInjection;

namespace DVG.AP.CMS.CarInfo.TestBase.Cores
{
    public abstract class XUnitTestBase
    {
        protected ServiceProvider Provider;

        protected XUnitTestBase(IServiceCollection services)
        {
            Provider = SetupFactory.SetupConfiguration(services);
        }

        protected XUnitTestBase()
        {
        }
    }
}
