using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetList;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetListUsingByCarSpec;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.CarPropertyComboboxOption, CarPropertyComboboxOptionGetListUsedVm>();
        CreateMap<Domain.Entities.CarPropertyComboBox, CarPropertyComboBoxGetListUsedVm>()
            .ForMember(dest => dest.CarPropertyComboboxOptions,
                opt => opt.MapFrom(src => src.CarPropertyComboboxOptions));

        CreateMap<Domain.Entities.CarPropertyComboBox, CarPropertyComboBoxGetListVm>();
        CreateMap<Domain.Entities.CarPropertyComboBox, CarPropertyComboBoxFilterVm>();

        CreateMap<Domain.Entities.CarPropertyComboBox, CarPropertyComboBoxDetail>();
        CreateMap<Domain.Entities.CarPropertyComboboxOption, CarPropertyComboBoxOptionDetailVm>();


        CreateMap<CarPropertyComboBoxOptionForCreation, Domain.Entities.CarPropertyComboboxOption>();
        CreateMap<CarPropertyComboBoxForCreation, Domain.Entities.CarPropertyComboBox>();

        CreateMap<CarPropertyComboBoxOptionForUpdate, Domain.Entities.CarPropertyComboboxOption>();
        CreateMap<CarPropertyComboBoxForUpdate, Domain.Entities.CarPropertyComboBox>();
    }
}