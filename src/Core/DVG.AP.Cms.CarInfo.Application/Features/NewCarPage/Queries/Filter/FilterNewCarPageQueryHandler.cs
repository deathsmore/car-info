using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Dtos.Common;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Common;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.UserSpec;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Queries.Filter
{
    public class FilterNewCarPageQueryHandler : IRequestHandler<FilterNewCarPageQuery, PagedList<NewCarPageFilterVm>>
    {
        private readonly INewCarPageRepository _newCarPageRepositoy;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public FilterNewCarPageQueryHandler(
            INewCarPageRepository newCarPageRepositoy, IMapper mapper,
            IUserRepository userRepository)
        {
            _newCarPageRepositoy = newCarPageRepositoy;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<PagedList<NewCarPageFilterVm>> Handle(FilterNewCarPageQuery request,
            CancellationToken cancellationToken)
        {
            var filterParam = request.FilterParam;
            var promotionPages = await _newCarPageRepositoy.FilterAsync(filterParam);

            if (promotionPages.Collections is null || !promotionPages.Collections.Any())
            {
                return new PagedList<NewCarPageFilterVm>();
            }

            #region Lấy thông tin user activity
            await GetUserActivities(promotionPages.Collections);
            #endregion
            return promotionPages;
        }

        public async Task GetUserActivities(IEnumerable<NewCarPageFilterVm> newCars)
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
