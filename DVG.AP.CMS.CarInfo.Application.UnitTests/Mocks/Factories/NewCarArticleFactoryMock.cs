using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ServiceLocators;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks.Factories
{
    // public static class NewCarArticleFactoryMock
    // {
    //     public static Mock<NewCarArticleFactory> Get()
    //     {
    //         var newCarArticleFactory = new Mock<NewCarArticleFactory>();
    //         
    //         newCarArticleFactory.Setup(fac => fac.CreateNewCarArticle(
    //             It.IsAny<NewCarArticleType>()
    //             )
    //         ).ReturnsAsync((
    //             NewCarArticleType newCarArticleType
    //             ) =>
    //         {
    //             return new NewCarArticleBrand();
    //         });
    //         return newCarArticleFactory;
    //     }
    // }
}
