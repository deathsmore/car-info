using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Dtos.Common;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Common;
using DVG.AP.Cms.Common.Api.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.GrpcPersistence.Repositories.Common
{
    public class UserRepository : IUserRepository
    {
        private readonly UserGrpc.UserGrpcClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserGrpc.UserGrpcClient client, IMapper mapper, ILogger<UserRepository> logger)
        {
            _client = client;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PagedList<UserFilterDto>> Gets(UserFilterParam param)
        {
            try
            {
                var grpcRequest = _mapper.Map<UserFilterRequest>(param);
                var response = await _client.GetsAsync(grpcRequest);

                var collection = _mapper.Map<List<UserFilterDto>>(response.Collections);
                return new PagedList<UserFilterDto>(1, -1, response.TotalCount, collection); //Đang phải gán tường minh vì ko auto map đc vào PagedList (collection readonly)
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
