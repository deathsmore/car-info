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
    public interface INewCarModelRepository : IRepository<NewCarModel>
    {
        Task<int> CountActiveArticlesByBrandAsync(int brandId);
        Task<PagedList<NewCarArticleFilterDto>> FilterAsync(List<long> ids, NewCarArticleFilterParam param);
        Task<NewCarModel> GetByModelId(int modelId);
        Task<NewCarModel> GetDetailAsync(long id);
    }
}
