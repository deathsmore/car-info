using DVG.AP.Cms.CarInfo.Application.Contracts.Dtos.Common;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Common;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;
using DVG.AP.Cms.CarInfo.Domain.Enums;

using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.Filter;

public class
    FilterNewCarArticleQueryHandler : IRequestHandler<FilterNewCarArticleQuery, PagedList<NewCarArticleFilterDto>>
{
    private readonly NewCarArticleFactory _newCarArticleFactory;
    private readonly ISeoInfoRepository _seoInfoRepository;
    private readonly IUserRepository _userRepository;

    public FilterNewCarArticleQueryHandler(
        NewCarArticleFactory newCarArticleFactory,
        ISeoInfoRepository seoInfoRepository,
        IUserRepository userRepository
        )
    {
        _newCarArticleFactory = newCarArticleFactory;
        _seoInfoRepository = seoInfoRepository;
        _userRepository = userRepository;
    }


    public async Task<PagedList<NewCarArticleFilterDto>> Handle(FilterNewCarArticleQuery request,
        CancellationToken cancellationToken)
    {
        var newCarArticle = _newCarArticleFactory.CreateNewCarArticle(request.NewCarArticleFilterParam.Type);
        if (request.NewCarArticleFilterParam.ContentAngleTag != 0 ||
            request.NewCarArticleFilterParam.ContentFormatTag != ContentFormatTag.All ||
            request.NewCarArticleFilterParam.SourceTag != SourceTag.All)
        {
            var restParam = new NewCarArticleRestParam
            {
                NewCarArticleIds = await _seoInfoRepository.GetListNewCarArticleId(request.NewCarArticleFilterParam.ContentFormatTag,
                    request.NewCarArticleFilterParam.ContentAngleTag, request.NewCarArticleFilterParam.SourceTag)
            };
            newCarArticle.SetRestParam(restParam);
        }

        var result = await newCarArticle.FilterAsync(request.NewCarArticleFilterParam);
        if (result.Collections is null || !result.Collections.Any())
        {
            return new PagedList<NewCarArticleFilterDto>();
        }

        #region Lấy thông tin user activity
        await GetUserActivities(result.Collections);
        #endregion
        return result;
    }

    public async Task GetUserActivities(IEnumerable<NewCarArticleFilterDto> newCars)
    {
        try
        {
            var userIds = new List<int>();
            userIds.AddRange(newCars.Select(x => x.CreatedBy));
            userIds.AddRange(newCars.Select(x => x.ModifiedBy));

            var usersActivities = userIds.Any() ? (await _userRepository.Gets(new UserFilterParam()
            {
                UserIds = userIds.Distinct().ToArray(),
                IsPaging = false
            }))?.Collections : null;

            if (usersActivities != null && usersActivities.Any())
            {
                foreach(var newCar in newCars)
                {
                    newCar.CreatedByName = usersActivities.FirstOrDefault(u => u.UserId == newCar.CreatedBy)?.UserName;
                    newCar.ModifiedByName = usersActivities.FirstOrDefault(u => u.UserId == newCar.ModifiedBy)?.UserName;
                }
            }
        }
        catch(Exception ex)
        {

        }
    }
}