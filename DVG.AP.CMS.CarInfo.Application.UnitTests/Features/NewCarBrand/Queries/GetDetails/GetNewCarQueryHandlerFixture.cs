using System.Collections.Generic;
using System.Linq;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail;
using DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks.Repositories;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Moq;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBrand.Queries.GetDetails
{
    public class GetNewCarQueryHandlerFixture  : ApplicationUnitTestBase
    {
        public GetDetailNewCarArticleQueryHandler GetDetailNewCarArticleQueryHandler;

        public GetNewCarQueryHandlerFixture()
        {
            var newCarArticleBase = new List<NewCarArticleBase>
            {
                new Cms.CarInfo.Domain.Entities.NewCarBrand(){Id = 1, Title = "Title 1"},
                new Cms.CarInfo.Domain.Entities.NewCarBrand(){Id = 2, Title = "Title 2"},
                new Cms.CarInfo.Domain.Entities.NewCarBrand(){Id = 3, Title = "Title 3"}
            };

            Mock<NewCarArticleAbstract> absMock = new Mock<NewCarArticleAbstract>();
            absMock.Setup(c => c.GetDetailAsync(It.IsAny<long>()))!
                .ReturnsAsync((long id) => newCarArticleBase.FirstOrDefault(c => c.Id == id));
            
            absMock
                .Setup(c => c.SetNewCarEntity(It.IsAny<NewCarArticleBase>()))
                .CallBase();
            
            Mock<NewCarArticleFactory> fatoryMock = new Mock<NewCarArticleFactory>();
            fatoryMock.Setup(fac => fac.CreateNewCarArticle(
                    It.IsAny<NewCarArticleType>()
                )
            ).Returns((
                NewCarArticleType newCarArticleType
            ) => absMock.Object);

            var demo = fatoryMock.Object;
            
            GetDetailNewCarArticleQueryHandler =   new GetDetailNewCarArticleQueryHandler(mapper, UrlRepositoryMock.Get().Object, demo);
            
        }
    }
}
