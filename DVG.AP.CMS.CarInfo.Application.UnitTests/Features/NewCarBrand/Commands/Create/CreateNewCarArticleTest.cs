using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DVG.AutoPortal.Core.Exceptions;
using Xunit;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Features.NewCarBrand.Commands.Create
{
    public  class CreateNewCarArticleTest : IClassFixture<CreateNewCarArticleCommandHandlerFixture>
    {
        private readonly CreateNewCarArticleCommandHandlerFixture _handlerFixture;
        public CreateNewCarArticleTest(CreateNewCarArticleCommandHandlerFixture handlerFixture)
        {
            _handlerFixture = handlerFixture;
        }

        // Check validation
        [Fact]
        public async Task Can_Create_ExceptionValidation_TitleMaxLength256()
        {
            //Arrange 
            var param = new CreateNewCarArticleCommand()
            {
                NewCarArticle = new NewCarArticleForCreation()
                {
                    BrandId = 60,
                    Title = new string('t', 300),
                    Type = NewCarArticleType.Brand,
                    NewCarSEOInfos = new NewCarSEOInfos()
                    {
                        ContentAngleTag = 1,
                        ContentFormatTag = ContentFormatTag.Article,
                        SourceTag = SourceTag.Editoral
                    },
                    PublishedDate = DateTime.Now
                }
            };
            //Act
            Task Act() => _handlerFixture.CreateNewCarArticleCommandHandler.Handle(param, CancellationToken.None);
            await Assert.ThrowsAsync<ValidationException>(Act);

        }
        
        // Check điều kiện VD: check exist new car brand
        [Fact]
        public async Task Can_Create_ExceptionBrandExist()
        {
            //Arrange 
            var param = new CreateNewCarArticleCommand()
            {
                NewCarArticle = new NewCarArticleForCreation()
                {
                    BrandId = 10, // brand 10 exist
                    Title = new string('t', 250),
                    Type = NewCarArticleType.Brand,
                    NewCarSEOInfos = new NewCarSEOInfos()
                    {
                        ContentAngleTag = 1,
                        ContentFormatTag = ContentFormatTag.Article,
                        SourceTag = SourceTag.Editoral
                    },
                    PublishedDate = DateTime.Now
                }
            };
            //Act
            Task Act() => _handlerFixture.CreateNewCarArticleCommandHandler.Handle(param, CancellationToken.None);
            await Assert.ThrowsAsync<ConflictException>(Act);
        }

        // Check Create
        [Fact]
        public async Task Can_Create_NewCarBrandSuccess()
        {
            //Arrange 
            var param = new CreateNewCarArticleCommand()
            {
                NewCarArticle = new NewCarArticleForCreation()
                {
                    BrandId = 60,
                    Title = "Datsun Car 2022",
                    NewCarSEOInfos = new NewCarSEOInfos()
                    {
                        ContentAngleTag = 1,
                        ContentFormatTag = ContentFormatTag.Article,
                        SourceTag = SourceTag.Editoral
                    },
                    PublishedDate = DateTime.Now
                }
            };

            //Act
            var newCarBrandId = await _handlerFixture.CreateNewCarArticleCommandHandler.Handle(param, CancellationToken.None);

            //Assert
            Assert.True(newCarBrandId > 0);
        }
    }
}
