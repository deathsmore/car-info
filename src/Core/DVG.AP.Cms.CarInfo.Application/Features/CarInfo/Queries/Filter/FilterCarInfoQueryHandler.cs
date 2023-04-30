using DVG.AP.Cms.CarInfo.Application.Contracts.Dtos.Common;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Common;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.Filter
{
    public class FilterCarInfoQueryHandler : IRequestHandler<FilterCarInfoQuery, PagedList<FilterCarInfoVm>>
    {
        private readonly ICarInfoRepository _carInfoRepository;
        private readonly IUserRepository _userRepository;

        public FilterCarInfoQueryHandler(ICarInfoRepository carInfoRepository,
            IUserRepository userRepository)
        {
            _carInfoRepository = carInfoRepository;
            _userRepository = userRepository;
        }

        public async Task<PagedList<FilterCarInfoVm>> Handle(FilterCarInfoQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _carInfoRepository.FilterAsync(request.CarInfoParameter);
            if (result.Collections is null || !result.Collections.Any())
            {
                return new PagedList<FilterCarInfoVm>();
            }

            #region Lấy thông tin user activity
            await GetUserActivities(result.Collections);
            #endregion
            return result;
        }

        public async Task GetUserActivities(IEnumerable<FilterCarInfoVm> newCars)
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
                    foreach (var newCar in newCars)
                    {
                        newCar.CreatedByName = usersActivities.FirstOrDefault(u => u.UserId == newCar.CreatedBy)?.UserName;
                        newCar.ModifiedByName = usersActivities.FirstOrDefault(u => u.UserId == newCar.ModifiedBy)?.UserName;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
