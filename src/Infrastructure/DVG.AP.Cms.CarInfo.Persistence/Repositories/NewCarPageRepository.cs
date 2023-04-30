using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Queries.Filter;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    public class NewCarPageRepository : Repository<NewCarPage>, INewCarPageRepository
    {
        public NewCarPageRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedList<NewCarPageFilterVm>> FilterAsync(NewCarPageFilterParam paramFilter)
        {
            var keywordSearch = ($"%{paramFilter.Keyword}%").ToLower();

            var query = from ncp in CarInfoDbContext.NewCarPages
                        where ((paramFilter.Status == null || ncp.Status == paramFilter.Status)
                              && (paramFilter.Type == null || ncp.Type == paramFilter.Type))
                              && (string.IsNullOrEmpty(paramFilter.Keyword) || EF.Functions.Like(ncp.Id.ToString().ToLower(), keywordSearch)
                                  || (ncp.Name != null && EF.Functions.Like(ncp.Name.ToLower(), keywordSearch))
                                  || (ncp.Title != null && EF.Functions.Like(ncp.Title.ToLower(), keywordSearch)))
                              
                        orderby ncp.LastModifiedDate descending, ncp.CreatedDate descending, ncp.Id descending
                        select new NewCarPageFilterVm()
                        {
                            Id = ncp.Id,
                            Title = ncp.Title,
                            Description = ncp.Description,
                            Type = ncp.Type,
                            MinPrice = ncp.MinPrice,
                            MaxPrice = ncp.MaxPrice,
                            Name = ncp.Name,
                            Status = ncp.Status,
                            Ordinal = ncp.Ordinal,
                            IsHot = ncp.IsHot,
                            ObjectId = ncp.ObjectId,
                            Slug = ncp.Slug,
                            CreatedDate = ncp.CreatedDate,
                            CreatedBy = ncp.CreatedBy,
                            ModifiedDate = ncp.LastModifiedDate.HasValue ? (DateTime)ncp.LastModifiedDate : DateTime.MinValue,
                            ModifiedBy = ncp.LastModifiedBy,
                        };

            query = query.AsNoTracking();

            var totalRecord = await query.CountAsync();
            if (totalRecord <= 0)
                return new PagedList<NewCarPageFilterVm>();

            var newCarPageVms = await query.Skip((paramFilter.PageNumber - 1) * paramFilter.PageSize)
                .Take(paramFilter.PageSize).ToListAsync();
            var result = new PagedList<NewCarPageFilterVm>(paramFilter.PageNumber, paramFilter.PageSize, totalRecord, newCarPageVms);
            return result;
        }
    }
}
