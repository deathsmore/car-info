using System.Threading;
using System.Threading.Tasks;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Xunit;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBrand.Queries.GetDetails;

public class GetDetailTests : IClassFixture<GetNewCarQueryHandlerFixture>
{
    private readonly GetNewCarQueryHandlerFixture _handlerFixture;

    public GetDetailTests(GetNewCarQueryHandlerFixture handlerFixture)
    {
        _handlerFixture = handlerFixture;   
    }
    
    
    [Theory]
    [InlineData(1)]
    public async Task NewCarBrand_GetDetils_MustNotNull(int articleId)
    {
        //Arrange 
        var param = new GetDetailNewCarArticleQuery()
        {
            Id = articleId,
            Type = NewCarArticleType.Brand
        };
        //Act
        var newCarByBrand = await _handlerFixture.GetDetailNewCarArticleQueryHandler.Handle(param, CancellationToken.None);
        //Assert
        Assert.NotNull(newCarByBrand);
        Assert.True(newCarByBrand.Id == articleId.ToString());
    }
}
