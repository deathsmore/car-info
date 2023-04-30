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

public class NewCarArticleBrand : NewCarArticleAbstract
{
    public NewCarArticleBrand(NewCarServiceLocator serviceLocator) : base(serviceLocator)
    {
        Type = NewCarArticleType.Brand;
        ObjectType = ObjectType.NewCarBrand;
        this.NewCarEntity = new NewCarBrand();
    }

    public override async Task CalcPriceRange()
    {
        if (NewCarArticle != null)
        {
            var carInfosByBrand = await ServiceLocator.CarInfoRepository.GetListPriceRangeByBrand(NewCarArticle.ObjectId.ToString().ToInt());
            if (carInfosByBrand != null && carInfosByBrand.Any())
            {
                var minPrices = carInfosByBrand.Where(x => x.MinPrice > 0).Select(x => x.MinPrice).ToList();
                NewCarArticle.MinPrice = minPrices != null && minPrices.Any() ? minPrices.Min(x => x) : 0D;
                NewCarArticle.MaxPrice = carInfosByBrand.Max(x => x.MaxPrice);
            }
        }
    }

    public override async Task<PagedList<NewCarArticleFilterDto>> FilterAsync(NewCarArticleFilterParam filterParam)
    {
        var newCarPaging = await ServiceLocator.NewCarBrandRepository.FilterAsync(RestParam.NewCarArticleIds, filterParam);

        if (!newCarPaging.Collections.Any())
        {
            return new PagedList<NewCarArticleFilterDto>();
        }
        return newCarPaging;
    }

    public override async Task<NewCarArticleBase> GetDetailAsync(long id)
    {
        this.NewCarEntity = (NewCarBrand)(await ServiceLocator.NewCarBrandRepository.GetDetailAsync(id));
        return this.NewCarEntity;
    }
    
    public override async Task<NewCarArticleBase> Get(int id)
    {
        return await ServiceLocator.NewCarBrandRepository.GetByBrandId(id);
    }
    public override async Task<int> CountActiveArticles(int brandId)
    {
        return await ServiceLocator.NewCarModelRepository.CountActiveArticlesByBrandAsync(brandId);
    }

    public override async Task<long> Insert(NewCarArticleForCreation newCarArticle)
    {
        var checkExisted = await ServiceLocator.NewCarBrandRepository.GetByBrandId(newCarArticle.BrandId);
        if (checkExisted != null)
        {
            throw new ConflictException(nameof(Domain.Entities.NewCarBrand), null);
        }

        #region Check xem brand có bài newCarModel đang active on FE ko
        var countActiveNewCarModel = await ServiceLocator.NewCarModelRepository.CountActiveArticlesByBrandAsync(newCarArticle.BrandId);
        newCarArticle.HasNewCarModel = countActiveNewCarModel > 0;
        #endregion

        this.NewCarEntity = ServiceLocator.Mapper.Map<NewCarBrand>(newCarArticle);

        foreach (var content in this.NewCarEntity.Contents)
        {
            content.Type = NewCarArticleType.Brand;
            if (GoogleAMPStrategy != null)
            {
                content.GgAmpContent = GoogleAMPStrategy.ConvertContentToAMP(content.LongContent);
            }
        }

        await this.CalcPriceRange();


        this.NewCarEntity = await ServiceLocator.NewCarBrandRepository.AddAsync((NewCarBrand)this.NewCarEntity);
        await ServiceLocator.NewCarBrandRepository.SaveChangesAsync();

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
        return this.NewCarEntity.Id;
    }

    public override void SetNewCarEntity(NewCarArticleBase entity)
    {
        this.NewCarEntity = (NewCarBrand)entity;
    }

    public override async Task<long> Update(NewCarArticleForUpdate newCarArticle)
    {
        var article = await this.GetDetailAsync(newCarArticle.Id);
        this.SetNewCarEntity(article);
        NotFoundException.NotFound(this.NewCarEntity, name: nameof(NewCarBrand), key: newCarArticle.Id);

        var checkExisted = await ServiceLocator.NewCarBrandRepository.GetByBrandId(newCarArticle.BrandId);
        if (checkExisted != null && checkExisted.Id != newCarArticle.Id)
        {
            throw new ConflictException(nameof(Domain.Entities.NewCarBrand), null);
        }

        #region Check xem model có bài newCarModel đang active on FE ko
        var countActiveNewCarModel = await ServiceLocator.NewCarModelRepository.CountActiveArticlesByBrandAsync(newCarArticle.BrandId);
        newCarArticle.HasNewCarModel = countActiveNewCarModel > 0;
        #endregion

        ServiceLocator.Mapper.Map(newCarArticle, this.NewCarEntity, typeof(NewCarArticleForUpdate), typeof(NewCarBrand));

        foreach (var content in this.NewCarEntity.Contents)
        {
            content.Type = NewCarArticleType.Brand;
            if (GoogleAMPStrategy != null)
            {
                content.GgAmpContent = GoogleAMPStrategy.ConvertContentToAMP(content.LongContent);
            }
        }
        await this.CalcPriceRange();

        await ServiceLocator.NewCarBrandRepository.UpdateAsync((NewCarBrand)this.NewCarEntity);
        await ServiceLocator.NewCarBrandRepository.SaveChangesAsync();

        return this.NewCarEntity.Id;
    }
}