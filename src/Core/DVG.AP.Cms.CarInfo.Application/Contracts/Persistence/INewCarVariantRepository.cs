using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence
{
    public interface INewCarVariantRepository : IRepository<NewCarVariant>
    {
        Task<PagedList<NewCarArticleFilterDto>> FilterAsync(List<long> ids, NewCarArticleFilterParam param);
        Task<NewCarVariant> GetByVariantId(int variantId);
        Task<NewCarVariant> GetDetailAsync(long id);

        /// <summary>
        /// Đếm số bài newCarVariant đang active liên quan đến model (modelId)
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        Task<int> CountActiveArticlesByModelAsync(int modelId);
    }
}
