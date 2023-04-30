using DVG.AP.CMS.CarInfo.TestBase.Fake;
using Microsoft.Extensions.DependencyInjection;
namespace DVG.AP.CMS.CarInfo.TestBase.Cores
{
    public static class ServiceFactory
    {
        public static ServiceProvider SetupConfiguration()
        {
            var services = new ServiceCollection();
           
            var provider = SetupFactory.SetupConfiguration(services);
            return provider;
        }
    }
}
