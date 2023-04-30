using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.GetDetail;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CarInfoPropertyForCreation, Domain.Entities.CarInfoPropertyValue>();
        CreateMap<CarInfoPropertyForUpdate, Domain.Entities.CarInfoPropertyValue>();
        CreateMap<Domain.Entities.CarInfoPropertyValue, CarInfoPropertyValueDetailVm>();
    }
}