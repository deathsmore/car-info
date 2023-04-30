using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ServiceLocators;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Core.Exceptions;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;

public class NewCarArticleVariant : NewCarArticleAbstract
{
    public NewCarArticleVariant(NewCarServiceLocator serviceLocator) : base(serviceLocator)
    {
        Type = NewCarArticleType.Variant;
        ObjectType = ObjectType.NewCarVariant;
        this.NewCarEntity = new NewCarVariant();
    }

    public override async Task CalcPriceRange()
    {
        if (NewCarEntity != null)
        {
            var carInfosByVariant = await ServiceLocator.CarInfoRepository.GetListPriceRangeByVariant(((NewCarVariant)NewCarEntity).VariantId);
            if (carInfosByVariant != null && carInfosByVariant.Any())
            {
                var minPrices = carInfosByVariant.Where(x => x.MinPrice > 0).Select(x => x.MinPrice).ToList();
                NewCarEntity.MinPrice = minPrices != null && minPrices.Any() ? minPrices.Min(x => x) : 0D;
                NewCarEntity.MaxPrice = carInfosByVariant.Max(x => x.MaxPrice);

                var lastestCarInfo = carInfosByVariant.FirstOrDefault(x => x.IsLatest);
                if(lastestCarInfo != null)
                {
                    ((NewCarVariant)NewCarEntity).Price = await ServiceLocator.CarInfoRepository.GetListedPrice(lastestCarInfo.Id);
                }    
            }
        }
    }

    public override async Task<PagedList<NewCarArticleFilterDto>> FilterAsync(NewCarArticleFilterParam filterParam)
    {
        var newCarPaging = await ServiceLocator.NewCarVariantRepository.FilterAsync(RestParam.NewCarArticleIds, filterParam);

        if (!newCarPaging.Collections.Any())
        {
            return new PagedList<NewCarArticleFilterDto>();
        }
        return newCarPaging;
    }

    public override async Task<NewCarArticleBase> GetDetailAsync(long id)
    {
        this.NewCarEntity = (NewCarVariant)(await ServiceLocator.NewCarVariantRepository.GetDetailAsync(id));
        return this.NewCarEntity;
    }

    public override async Task<long> Insert(NewCarArticleForCreation newCarArticle)
    {
        var checkExisted = await ServiceLocator.NewCarVariantRepository.GetByVariantId(newCarArticle.VariantId);
        if (checkExisted != null)
        {
            throw new ConflictException(nameof(Domain.Entities.NewCarVariant), null);
        }

        this.NewCarEntity = ServiceLocator.Mapper.Map<NewCarVariant>(newCarArticle);

        foreach (var image in this.NewCarEntity.Images)
        {
            image.ImageOfObject = ImageOfObject.ImageOfNewCarVariant;
        }

        foreach (var content in this.NewCarEntity.Contents)
        {
            content.Type = NewCarArticleType.Variant;
            if(GoogleAMPStrategy != null)
            {
                content.GgAmpContent = GoogleAMPStrategy.ConvertContentToAMP(content.LongContent);
            }
        }

        await this.CalcPriceRange();
        //await ServiceLocator.NewCarVariantService.CalPrice((NewCarVariant)this.NewCarEntity);

        this.NewCarEntity = await ServiceLocator.NewCarVariantRepository.AddAsync((NewCarVariant)this.NewCarEntity);
        await ServiceLocator.NewCarVariantRepository.SaveChangesAsync();

        #region Lưu thông tin SEO liên quan
        //var seoInfo = ServiceLocator.Mapper.Map<SeoInfo>(newCarArticle.SEOInfo);
        //seoInfo.ObjectType = this.ObjectType;
        //await ServiceLocator.SEOInfoRepository.AddAsync(seoInfo);
        //await ServiceLocator.SEOInfoRepository.SaveChangesAsync();
        #endregion

        #region Lưu thông tin Url
        var slugExisted = await ServiceLocator.URLRepository.GetBySlug(newCarArticle.Url);
        await ServiceLocator.URLRepository.AddAsync(new Url
        {
            ObjectId = this.NewCarEntity.Id,
            ObjectType = this.ObjectType,
            Slug = slugExisted == null ? newCarArticle.Url : $"{newCarArticle.Url}-{this.NewCarEntity.Id}-{this.ObjectType.GetHashCode()}",
            CreatedDate = DateTime.Now
        });
        await ServiceLocator.URLRepository.SaveChangesAsync();
        #endregion

        #region Checking trường HasNewCarVariant ở bài newCarModel tương ứng
        if (NewCarEntity.Status == NewCarArticleStatus.Approved && NewCarEntity.PublishedDate <= DateTime.Now)
        {
            var newCarModelEntity = await ServiceLocator.NewCarModelRepository.GetByModelId(newCarArticle.ModelId);
            if (newCarModelEntity != null)
            {
                newCarModelEntity.HasNewCarVariant = true;
                await ServiceLocator.NewCarModelRepository.UpdateAsync(newCarModelEntity);
            }
        }
        #endregion

        return this.NewCarEntity.Id;
        
        
    }

    public override void SetNewCarEntity(NewCarArticleBase entity)
    {
        this.NewCarEntity = (NewCarVariant)entity;
    }

    public override async Task<long> Update(NewCarArticleForUpdate newCarArticle)
    {
        await this.GetDetailAsync(newCarArticle.Id);
        NotFoundException.NotFound(this.NewCarEntity, name: nameof(NewCarVariant), key: newCarArticle.Id);

        var checkExisted = await ServiceLocator.NewCarVariantRepository.GetByVariantId(newCarArticle.VariantId);
        if (checkExisted != null && checkExisted.Id != newCarArticle.Id)
        {
            throw new ConflictException(nameof(Domain.Entities.NewCarVariant), null);
        }

        ServiceLocator.Mapper.Map(newCarArticle, this.NewCarEntity, typeof(NewCarArticleForUpdate), typeof(NewCarVariant));
        foreach (var image in this.NewCarEntity.Images)
        {
            image.ImageOfObject = ImageOfObject.ImageOfNewCarVariant;
        }

        foreach (var content in this.NewCarEntity.Contents)
        {
            content.Type = NewCarArticleType.Variant;
            if (GoogleAMPStrategy != null)
            {
                content.GgAmpContent = GoogleAMPStrategy.ConvertContentToAMP(content.LongContent);
            }
        }
        await this.CalcPriceRange();

        await ServiceLocator.NewCarVariantRepository.UpdateAsync((NewCarVariant)this.NewCarEntity);
        await ServiceLocator.NewCarVariantRepository.SaveChangesAsync();


        #region Checking trường HasNewCarVariant ở bài newCarModel tương ứng
        var newCarModelEntity = await ServiceLocator.NewCarModelRepository.GetByModelId(newCarArticle.ModelId);
        if (newCarModelEntity != null)
        {
            var countActiveNewCarVariant = await ServiceLocator.NewCarVariantRepository.CountActiveArticlesByModelAsync(newCarArticle.ModelId);
            newCarModelEntity.HasNewCarVariant = countActiveNewCarVariant > 0;
            await ServiceLocator.NewCarModelRepository.UpdateAsync(newCarModelEntity);
        }
        #endregion


        return this.NewCarEntity.Id;
    }
}