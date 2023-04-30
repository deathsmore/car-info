using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Cores
{
    public class TestBase
    {
        protected readonly INewCarArticleRepository NewCarArticleRepository;
        protected readonly IBrandRepository BrandRepository;
        protected readonly IMediator Mediator;
        //protected readonly IRequestHandlerBase<ClientPromotionDetailDto, ClientPromotionDetailQuery> ClientPromotionHandler;

        public TestBase()
        {
            var serviceProvider = ServiceFactory.SetupConfiguration();
            NewCarArticleRepository = serviceProvider.GetService<INewCarArticleRepository>()!;
            Mediator = serviceProvider.GetService<IMediator>();
            //ClientPromotionHandler = serviceProvider.GetService<IRequestHandlerBase<ClientPromotionDetailDto, ClientPromotionDetailQuery>>();
        }
    }
}
