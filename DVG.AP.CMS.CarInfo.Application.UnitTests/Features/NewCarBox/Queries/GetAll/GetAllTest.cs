using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetAll;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBox.Queries.GetAll
{
    public class GetBoxSettingDetailTest : IClassFixture<GetBoxSettingDetailQueryHandlerFixture>
    {
        private readonly GetBoxSettingDetailQueryHandlerFixture handlerFixture;

        public GetBoxSettingDetailTest(GetBoxSettingDetailQueryHandlerFixture handlerFixture)
        {
            this.handlerFixture = handlerFixture;
        }

        [Fact]
        public async Task NewCarBox_GetAll_MustNotNull()
        {
            //Arrange 
            var param = new GetAllQuery()
            {
                
            };

            //Act
            var newCarBoxs = await this.handlerFixture.GetAllQueryHandler.Handle(param, CancellationToken.None);

            //Assert
            Assert.NotNull(newCarBoxs);
        }
    }
}
