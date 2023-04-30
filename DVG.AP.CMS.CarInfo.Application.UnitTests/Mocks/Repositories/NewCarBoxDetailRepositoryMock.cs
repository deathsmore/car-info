using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetBoxSettingDetail;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks.Repositories
{
    public static class NewCarBoxDetailRepositoryMock
    {
        public static Mock<INewCarBoxDetailRepository> Get()
        {
            var newCarBoxDetailRepository = new Mock<INewCarBoxDetailRepository>();
            var item = new NewCarBoxDetailItemDto()
            {
                Id = 1,
                NewCarArticleId = 1,
                NewCarArticleTitle = "Honda Civic 2022",
                NewCarBoxId = 1,
                NewCarStatus = Cms.CarInfo.Domain.Enums.NewCarArticleStatus.Approved,
                ObjectId = 46,
                ObjectType = Cms.CarInfo.Domain.Enums.NewCarArticleType.Brand,
                Ordinal = 1,
                PublishedDate = DateTime.Now
            };

            var listItems = new List<NewCarBoxDetailItemDto>();
            listItems.Add(item);
          
            newCarBoxDetailRepository.Setup(repo => repo.GetByNewCarBox(It.IsAny<int>()))
                .ReturnsAsync(listItems);

            //newCarBoxDetailRepository.Setup(repo => repo.GetByNewCarBox(It.IsAny<int>()))
            //    .Callback<int>((newCarArticleId) => {
            //        listItems.AsReadOnly();
            //    });

            return newCarBoxDetailRepository;
        }
    }
}
