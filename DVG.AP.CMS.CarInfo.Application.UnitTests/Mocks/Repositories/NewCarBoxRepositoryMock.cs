using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Domain.Entities;
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
    public static class NewCarBoxRepositoryMock
    {
        public static Mock<IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox>> Get()
        {
            var newCarBoxRepository = new Mock<IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox>>();
            newCarBoxRepository.Setup(repo => repo.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((CancellationToken cancellationToken) => new List<NewCarBox>()
                {
                    new NewCarBox()
                    {
                        Id = 1,
                        Name = "Xe bán chạy",
                        NewCarType = NewCarArticleType.Model,
                        NumberDisplay = 6,
                        Ordinal = 1,
                        Status = ActiveStatus.Active,
                    },
                    new NewCarBox()
                    {
                        Id = 2,
                        Name = "Xe mới",
                        NewCarType = NewCarArticleType.Model,
                        NumberDisplay = 6,
                        Ordinal = 1,
                        Status = ActiveStatus.Active,
                    }
                });

            newCarBoxRepository.Setup(c => c.GetByIdAsync(It.IsAny<int>() , It.IsAny<CancellationToken>())).ReturnsAsync((int id,CancellationToken cancellationToken ) => new NewCarBox()
            {
                Id = 1,
                Name = "Xe bán chạy",
                NewCarType = NewCarArticleType.Model,
                NumberDisplay = 6,
                Ordinal = 1,
                Status = ActiveStatus.Active,
            });
            return newCarBoxRepository;
        }
    }
}
