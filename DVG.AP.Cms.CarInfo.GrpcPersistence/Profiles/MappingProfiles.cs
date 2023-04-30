using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Dtos.Common;
using DVG.AP.Cms.Common.Api.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.GrpcPersistence.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region map from Common Dtos of CommonAPI to this projects's DTOs
            CreateMap<UserVm, UserFilterDto>();
            CreateMap<UserFilterParam, UserFilterRequest>();
            #endregion
        }
    }
}
