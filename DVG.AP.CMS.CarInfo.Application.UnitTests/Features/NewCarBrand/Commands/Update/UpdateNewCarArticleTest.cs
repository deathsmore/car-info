using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBrand.Commands.Update
{
    public class UpdateNewCarArticleTest : IClassFixture<UpdateNewCarArticleCommandHandlerFixture>
    {
        private readonly UpdateNewCarArticleCommandHandlerFixture _handlerFixture;
        public UpdateNewCarArticleTest(UpdateNewCarArticleCommandHandlerFixture handlerFixture)
        {
            _handlerFixture = handlerFixture;
        }

        // Check Validation
        [Fact]
        public async Task UpdateNewCarArticle_ExceptionValidation_TitleMaxLength256()
        {
            //Arrange 
            var newCarArticle = new NewCarArticleForUpdate()
            {
                BrandId = 10,
                Title = new string('t', 300),
                Type = NewCarArticleType.Brand,
                NewCarSEOInfos = new NewCarSEOInfos()
                {
                    ContentAngleTag = 1,
                    ContentFormatTag = ContentFormatTag.Article,
                    SourceTag = SourceTag.Editoral
                },
                PublishedDate = DateTime.Now,
                Status = NewCarArticleStatus.Approved
            };

            var param = new UpdateNewCarArticleCommand(newCarArticle, 1, 1)
            {
                NewCarArticle = newCarArticle,
            };

            //Act
            Task Act() => _handlerFixture.UpdateNewCarArticleCommandHandler.Handle(param, CancellationToken.None);
            await Assert.ThrowsAsync<ValidationException>(Act);

        }

        // Check Update
        [Fact]
        public async Task NewCarArticle_Update_MustSucees()
        {
            //Arrange 
            var newCarArticle = new NewCarArticleForUpdate()
            {
                BrandId = 10,
                Title = "Honda Car 2022",
                Type = NewCarArticleType.Brand,
                NewCarSEOInfos = new NewCarSEOInfos()
                {
                    ContentAngleTag = 1,
                    ContentFormatTag = ContentFormatTag.Article,
                    SourceTag = SourceTag.Editoral
                },
                Status = NewCarArticleStatus.Approved,
            };

            var param = new UpdateNewCarArticleCommand(newCarArticle, 1, 1)
            {
                NewCarArticle = newCarArticle
            };

            //Act
            var newCarBrandId = await _handlerFixture.UpdateNewCarArticleCommandHandler.Handle(param, CancellationToken.None);

            //Assert
            Assert.True(newCarBrandId > 0);
        }
    }
}
