using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetAll;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetBoxSettingDetail;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBox.Queries.GetBoxSettingDetail
{
    public class GetBoxSettingDetailTest : IClassFixture<GetBoxSettingDetailQueryHandlerFixture>
    {
        private readonly GetBoxSettingDetailQueryHandlerFixture handlerFixture;

        public GetBoxSettingDetailTest(GetBoxSettingDetailQueryHandlerFixture handlerFixture)
        {
            this.handlerFixture = handlerFixture;
        }

        [Theory]
        [InlineData(1)]
        public async Task NewCarBox_GetDetail_MustNotNull(int newCarBoxId)
        {
            //Arrange 
            var param = new GetBoxSettingDetailQuery(newCarBoxId)
            {
                
            };

            //Act
            var newCarBoxDetails = await this.handlerFixture.GetBoxSettingDetailQueryHandler.Handle(param, CancellationToken.None);

            //Assert
            Assert.NotNull(newCarBoxDetails);
        }
    }
}
