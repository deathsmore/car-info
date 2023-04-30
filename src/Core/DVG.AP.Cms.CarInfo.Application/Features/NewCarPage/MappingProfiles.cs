using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Commands.CreateNewCarPage;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Commands.UpdateNewCarPage;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Queries.GetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarPage
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Domain.Entities.NewCarPage, NewCarPageDetailVm>();
            CreateMap<NewCarPageForCreation, Domain.Entities.NewCarPage>();
            CreateMap<NewCarPageForUpdate, Domain.Entities.NewCarPage>();
        }
    }
}
