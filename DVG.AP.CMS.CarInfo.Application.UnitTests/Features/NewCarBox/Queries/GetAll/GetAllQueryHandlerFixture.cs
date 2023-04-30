using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetAll;
using DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBox.Queries.GetAll
{
    public class GetBoxSettingDetailQueryHandlerFixture : ApplicationUnitTestBase
    {
        public GetAllQueryHandler GetAllQueryHandler;

        public GetBoxSettingDetailQueryHandlerFixture()
        {
            GetAllQueryHandler = new GetAllQueryHandler(NewCarBoxRepositoryMock.Get().Object);
        }
    }
}
