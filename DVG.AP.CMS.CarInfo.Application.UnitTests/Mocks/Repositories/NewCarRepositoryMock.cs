using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.NewCarArticle;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.UrlSpec;
using DVG.AP.Cms.CarInfo.Domain.Entities;
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
    public static class NewCarRepositoryMock
    {
        public static Mock<IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarArticle>> Get()
        {
            var newCarRepository = new Mock<IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarArticle>>();
            newCarRepository.Setup(repo => repo.GetBySpecAsync(
                It.IsAny<NewCarArticleSpec>(),
                It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync((NewCarArticleSpec newCarArticleSpec, CancellationToken  cancelToken) =>
                new NewCarArticle()
                {
                    Id = 1,
                    AuthorId = 1,
                    Title = "bai viet"
                }
            );
            return newCarRepository;
        }
    }
}
