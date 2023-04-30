using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.UrlSpec;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks.Repositories
{
    public static class UrlRepositoryMock
    {
        public static Mock<IUrlRepository> Get()
        {
            var urlRepository = new Mock<IUrlRepository>();
            urlRepository.Setup(repo => repo.GetBySpecAsync(
                It.IsAny<UrlNewCarSpec>(),
                It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync((UrlNewCarSpec urlNewCarSpec, CancellationToken  cancelToken) =>
                new Url()
                {
                    Id = 1,
                    ObjectId = 1,
                    CreatedDate = DateTime.Now,
                    ObjectType = ObjectType.NewCar,
                    Slug = String.Empty
                }
            );
            return urlRepository;
        }
    }
}
