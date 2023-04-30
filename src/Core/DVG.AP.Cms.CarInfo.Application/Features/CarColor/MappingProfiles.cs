using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Commands.CarColorInsert;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Commands.CarColorUpdate;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.GetColors;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.CarColor, CarColorInsert>().ReverseMap();
        CreateMap<Domain.Entities.CarColor, CarColorUpdate>().ReverseMap();
        CreateMap<Domain.Entities.CarColor, FilterCarColorVm>().ReverseMap();
        CreateMap<Domain.Entities.CarColor, GetAllCarColorVm>().ReverseMap();
    }
}