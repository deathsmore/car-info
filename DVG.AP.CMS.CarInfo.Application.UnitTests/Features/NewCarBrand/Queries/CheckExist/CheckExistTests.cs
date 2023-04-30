using System.Threading;
using System.Threading.Tasks;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.CheckExist;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Xunit;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBrand.Queries.CheckExist;

public class CheckExistTests : IClassFixture<CheckExistQueryHandlerFixture>
{
    private readonly CheckExistQueryHandlerFixture _handlerFixture;

    public CheckExistTests(CheckExistQueryHandlerFixture handlerFixture)
    {
        
        _handlerFixture = handlerFixture;   
    }


    [Theory]
    [InlineData("1")]
    public async Task NewCarBrand_CheckExist_MustExist(string objId)
    {
        //Arrange 
        var param = new CheckExistQuery()
        {
            ObjectId = objId,
            Type = NewCarArticleType.Brand
        };
        //Act
        var newCarArticle = await _handlerFixture.CheckExistQueryHandler.Handle(param, CancellationToken.None);
        //Assert

        Assert.NotNull(newCarArticle);
    }
}
