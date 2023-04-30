using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetAll;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetBoxSettingDetail;
using DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks.Repositories;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBox.Queries.GetBoxSettingDetail
{
    public class GetBoxSettingDetailQueryHandlerFixture : ApplicationUnitTestBase
    {
        public GetBoxSettingDetailQueryHandler GetBoxSettingDetailQueryHandler;

        public GetBoxSettingDetailQueryHandlerFixture()
        {
            GetBoxSettingDetailQueryHandler = new GetBoxSettingDetailQueryHandler(NewCarBoxRepositoryMock.Get().Object, NewCarBoxDetailRepositoryMock.Get().Object, mapper);
        }
    }
}
