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

public class NewCarArticleModel : NewCarArticleAbstract
{
    public NewCarArticleModel(NewCarServiceLocator serviceLocator) : base(serviceLocator)
    {
        Type = NewCarArticleType.Model;
        ObjectType = ObjectType.NewCarModel;
        this.NewCarEntity = new NewCarModel();
    }


    public override async Task CalcPriceRange()
    {
        if (NewCarEntity != null)
        {
            var carInfosByModel = await ServiceLocator.CarInfoRepository.GetListPriceRangeByModel(((NewCarModel)NewCarEntity).ModelId);
            if (carInfosByModel != null && carInfosByModel.Any())
            {
                var minPrices = carInfosByModel.Where(x=> x.MinPrice > 0).Select(x=> x.MinPrice).ToList();
                NewCarEntity.MinPrice = minPrices != null && minPrices.Any() ? minPrices.Min(x => x) : 0D;
                NewCarEntity.MaxPrice = carInfosByModel.Max(x => x.MaxPrice);
            }
        }
    }

    public override async Task<PagedList<NewCarArticleFilterDto>> FilterAsync(NewCarArticleFilterParam filterParam)
    {
        var newCarPaging = await ServiceLocator.NewCarModelRepository.FilterAsync(RestParam.NewCarArticleIds,filterParam);

        if (!newCarPaging.Collections.Any())
        {
            return new PagedList<NewCarArticleFilterDto>();
        }
        return newCarPaging;
    }

    public override async Task<NewCarArticleBase> GetDetailAsync(long id)
    {
        this.NewCarEntity = (NewCarModel)(await ServiceLocator.NewCarModelRepository.GetDetailAsync(id));
        return this.NewCarEntity;
    }

    public override async Task<long> Insert(NewCarArticleForCreation newCarArticle)
    {
        var checkExisted = await ServiceLocator.NewCarModelRepository.GetByModelId(newCarArticle.ModelId);
        if (checkExisted != null)
        {
            throw new ConflictException(nameof(Domain.Entities.NewCarModel), null);
        }

        #region Check xem model có bài newCarVariant đang active on FE ko
        var countActiveNewCarVariant = await ServiceLocator.NewCarVariantRepository.CountActiveArticlesByModelAsync(newCarArticle.ModelId);
        newCarArticle.HasNewCarVariant = countActiveNewCarVariant > 0;
        #endregion

        this.NewCarEntity = ServiceLocator.Mapper.Map<NewCarModel>(newCarArticle);
        foreach (var image in this.NewCarEntity.Images)
        {
            image.ImageOfObject = ImageOfObject.ImageOfNewCarModel;
        }

        foreach (var content in this.NewCarEntity.Contents)
        {
            content.Type = NewCarArticleType.Model;
            if (GoogleAMPStrategy != null)
            {
                content.GgAmpContent = GoogleAMPStrategy.ConvertContentToAMP(content.LongContent);
            }
        }
        
        await this.CalcPriceRange();

        this.NewCarEntity = await ServiceLocator.NewCarModelRepository.AddAsync((NewCarModel)this.NewCarEntity);
        await ServiceLocator.NewCarModelRepository.SaveChangesAsync();

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
        //base.Insert(newCarArticle);
        return this.NewCarEntity.Id;       
    }

    public override void SetNewCarEntity(NewCarArticleBase entity)
    {
        this.NewCarEntity = (NewCarModel)entity;
    }

    public override async Task<long> Update(NewCarArticleForUpdate newCarArticle)
    {
        await this.GetDetailAsync(newCarArticle.Id);
        NotFoundException.NotFound(this.NewCarEntity, name: nameof(NewCarModel), key: newCarArticle.Id);

        var checkExisted = await ServiceLocator.NewCarModelRepository.GetByModelId(newCarArticle.ModelId);
        if (checkExisted != null && checkExisted.Id != newCarArticle.Id)
        {
            throw new ConflictException(nameof(Domain.Entities.NewCarModel), null);
        }

        #region Check xem model có bài newCarVariant đang active on FE ko
        var countActiveNewCarVariant = await ServiceLocator.NewCarVariantRepository.CountActiveArticlesByModelAsync(newCarArticle.ModelId);
        newCarArticle.HasNewCarVariant = countActiveNewCarVariant > 0;
        #endregion

        ServiceLocator.Mapper.Map(newCarArticle, this.NewCarEntity, typeof(NewCarArticleForUpdate), typeof(NewCarModel));
        foreach (var image in this.NewCarEntity.Images)
        {
            image.ImageOfObject = ImageOfObject.ImageOfNewCarModel;
        }

        foreach (var content in this.NewCarEntity.Contents)
        {
            content.Type = NewCarArticleType.Model;
            if (GoogleAMPStrategy != null)
            {
                content.GgAmpContent = GoogleAMPStrategy.ConvertContentToAMP(content.LongContent);
            }
        }
        
        await this.CalcPriceRange();

        await ServiceLocator.NewCarModelRepository.UpdateAsync((NewCarModel)this.NewCarEntity);
        await ServiceLocator.NewCarModelRepository.SaveChangesAsync();

        return this.NewCarEntity.Id;
    }
}