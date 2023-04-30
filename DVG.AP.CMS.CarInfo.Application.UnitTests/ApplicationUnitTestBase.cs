using AutoMapper;
using DVG.AP.Cms.CarInfo.Application;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Persistence;
using DVG.AP.CMS.CarInfo.TestBase.Fake;
//using DVG.AP.CMS.CarInfo.TestBase.Fake;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests;

    
public abstract class ApplicationUnitTestBase
{
    protected ServiceProvider ServiceProvider;
    protected IMediator mediator;
    protected IMapper mapper;
   // protected NewCarArticleFactory fatory;

  
    public ApplicationUnitTestBase()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();
        ServiceProvider = SetupFactory.SetupConfiguration(services);
        
        mediator = ServiceProvider.GetService<IMediator>()!;
        mapper = ServiceProvider.GetService<IMapper>()!;
        //fatory = ServiceProvider.GetService<NewCarArticleFactory>()!;
        // DbContext = ServiceProvider.GetService<CarInfoDbContext>()!;
        
    }
}
  