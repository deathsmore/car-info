using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Helpers.OrderHelper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail;
using DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks.Repositories;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Moq;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.CheckExist;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBrand.Queries
{
    public class CheckExistQueryHandlerFixture : ApplicationUnitTestBase
    {
        public CheckExistQueryHandler CheckExistQueryHandler;

        public CheckExistQueryHandlerFixture()
        {
            CheckExistQueryHandler =   new CheckExistQueryHandler(NewCarRepositoryMock.Get().Object, mapper);
        }
    }
}
