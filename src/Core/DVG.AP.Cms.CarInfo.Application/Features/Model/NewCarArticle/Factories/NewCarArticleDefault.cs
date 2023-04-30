using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ServiceLocators;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;

public class NewCarArticleDefault : NewCarArticleAbstract
{
   
    public NewCarArticleDefault(NewCarServiceLocator serviceLocator) : base(serviceLocator)
    {
    }

    public override Task CalcPriceRange()
    {
        throw new NotImplementedException();
    }

    public override async Task<PagedList<NewCarArticleFilterDto>> FilterAsync(NewCarArticleFilterParam filterParam)
    {
        List<int> variantIds = new List<int>();
        if (filterParam.VariantId > 0)
        {
            variantIds.Add(filterParam.VariantId);
        }
        else
        {
            variantIds = filterParam.ModelId > 0 ?
                await ServiceLocator.VariantRepository.GetListIdByModelIds(new List<int> { filterParam.ModelId }) :
                await ServiceLocator.VariantRepository.GetListIdByBrandIds(filterParam.BrandId > 0 ? new List<int> { filterParam.BrandId } : null);
        }

        var pagedListNewCarVms = await ServiceLocator.NewCarArticleRepository.GetPagedListAllTypeAsync(RestParam.NewCarArticleIds, filterParam, variantIds);
        if (!pagedListNewCarVms.Collections.Any())
        {
            return new PagedList<NewCarArticleFilterDto>();
        }

        //var brandIds = pagedListNewCarVms.Collections.Where(x => x.Type == NewCarArticleType.Brand).Select(nc => nc.ObjectId.ToInt()).Distinct().ToList();
        //var modelIds = pagedListNewCarVms.Collections.Where(x => x.Type == NewCarArticleType.Model).Select(nc => nc.ObjectId.ToInt()).Distinct().ToList();
        //var variantIdsResult = pagedListNewCarVms.Collections.Where(x => x.Type == NewCarArticleType.Variant).Select(nc => nc.ObjectId.ToInt()).Distinct().ToList();
        //var carStructures = await ServiceLocator.VariantRepository.ListCarStructureDtoAsync(brandIds: brandIds, modelIds: modelIds, variantIds: variantIdsResult);
        //foreach (var newCarArticle in pagedListNewCarVms.Collections)
        //{
        //    switch (newCarArticle.Type)
        //    {
        //        case NewCarArticleType.Variant:
        //            var variantId = newCarArticle.ObjectId.ToInt();
        //            var variant = carStructures.FirstOrDefault(ct => ct.VariantId == variantId);
        //            newCarArticle.BrandName = variant?.BrandName;
        //            newCarArticle.ModelName = variant?.ModelName;
        //            newCarArticle.VariantName = variant?.VariantName;
        //            break;
        //        case NewCarArticleType.Model:
        //            var modelId = newCarArticle.ObjectId.ToInt();
        //            var model = carStructures.FirstOrDefault(ct => ct.ModelId == modelId);
        //            newCarArticle.BrandName = model?.BrandName;
        //            newCarArticle.ModelName = model?.ModelName;
        //            break;
        //        case NewCarArticleType.Brand:
        //            var brandId = newCarArticle.ObjectId.ToInt();
        //            var brand = carStructures.FirstOrDefault(ct => ct.BrandId == brandId);
        //            newCarArticle.BrandName = brand?.BrandName;
        //            break;
        //    }
           
        //}

        return pagedListNewCarVms;
    }

    public override Task<NewCarArticleBase> GetDetailAsync(long id)
    {
        throw new NotImplementedException();
    }

    public override Task<long> Insert(NewCarArticleForCreation newCarArticle)
    {
        throw new NotImplementedException();
    }

    public override void SetNewCarEntity(NewCarArticleBase entity)
    {
        throw new NotImplementedException();
    }

    public override Task<long> Update(NewCarArticleForUpdate newCarArticle)
    {
        throw new NotImplementedException();
    }
}