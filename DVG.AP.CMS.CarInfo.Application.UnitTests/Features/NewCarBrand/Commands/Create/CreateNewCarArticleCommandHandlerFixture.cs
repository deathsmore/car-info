using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBrand.Commands.Create
{
    public class CreateNewCarArticleCommandHandlerFixture : ApplicationUnitTestBase
    {
        public CreateNewCarArticleCommandHandler CreateNewCarArticleCommandHandler;
        public CreateNewCarArticleCommandHandlerFixture()
        {
            var newCarArticleBase = new List<NewCarArticleBase>
            {
                new Cms.CarInfo.Domain.Entities.NewCarBrand(){Id = 1, Title = "Title 1", BrandId = 1},
                new Cms.CarInfo.Domain.Entities.NewCarBrand(){Id = 2, Title = "Title 2", BrandId = 2},
                new Cms.CarInfo.Domain.Entities.NewCarBrand(){Id = 3, Title = "Title 3", BrandId = 3}
            };
            int idExist = 10;

            Mock<NewCarArticleAbstract> absMock = new Mock<NewCarArticleAbstract>();
            
            absMock.Setup(c => c.GetDetailAsync(It.IsAny<long>()))!
                .ReturnsAsync((long id) => newCarArticleBase.FirstOrDefault(c => c.Id == id));
           
            absMock.Setup(c => c.Get(It.IsAny<int>()))!
                .ReturnsAsync((int id) => id== idExist ? new NewCarArticleBase() : null);
            
            absMock.Setup(c => c.Insert(It.IsAny<NewCarArticleForCreation>()))!
                .CallBase();
            
            Mock<NewCarArticleFactory> fatoryMock = new Mock<NewCarArticleFactory>();
            fatoryMock.Setup(fac => fac.CreateNewCarArticle(
                    It.IsAny<NewCarArticleType>()
                )
            ).Returns((
                NewCarArticleType newCarArticleType
            ) => absMock.Object);

            CreateNewCarArticleCommandHandler = new CreateNewCarArticleCommandHandler(fatoryMock.Object);
        }
    }
}
