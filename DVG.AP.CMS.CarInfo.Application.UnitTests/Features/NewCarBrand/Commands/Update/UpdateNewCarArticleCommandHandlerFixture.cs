using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBrand.Commands.Update
{
    public class UpdateNewCarArticleCommandHandlerFixture : ApplicationUnitTestBase
    {
        public UpdateNewCarArticleCommandHandler UpdateNewCarArticleCommandHandler;

        public UpdateNewCarArticleCommandHandlerFixture()
        {
            var newCarArticleBase = new List<NewCarArticleBase>
            {
                new Cms.CarInfo.Domain.Entities.NewCarBrand(){Id = 1, Title = "Title 1"},
                new Cms.CarInfo.Domain.Entities.NewCarBrand(){Id = 2, Title = "Title 2"},
                new Cms.CarInfo.Domain.Entities.NewCarBrand(){Id = 3, Title = "Title 3"}
            };

            var newCarBase = new NewCarArticleBase()
            {
                Id = 1,
                Title = "new car base"
            };

            Mock<NewCarArticleAbstract> absMock = new Mock<NewCarArticleAbstract>();

            absMock.Setup(c => c.SetNewCarEntity(It.IsAny<NewCarArticleBase>()))!
                .CallBase();

            absMock.SetupAllProperties();

            absMock.Setup(c => c.GetDetailAsync(It.IsAny<long>()))!
                .ReturnsAsync((long id) => newCarArticleBase.FirstOrDefault(c => c.Id == id));

            Mock<NewCarArticleFactory> fatoryMock = new Mock<NewCarArticleFactory>();
            fatoryMock.Setup(fac => fac.CreateNewCarArticle(
                    It.IsAny<NewCarArticleType>()
                )
            ).Returns((
                NewCarArticleType newCarArticleType
            ) => absMock.Object);

            UpdateNewCarArticleCommandHandler = new UpdateNewCarArticleCommandHandler(UrlRepositoryMock.Get().Object, fatoryMock.Object);
        }
    }
}
