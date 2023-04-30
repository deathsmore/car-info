using AutoMapper;
using DVG.AP.Cms.CarInfo.Domain.Entities;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Commands.SetDetails
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewCarBoxDetailItem, NewCarBoxDetail>()
                .ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => src.ObjectId.ToLong()))
                ;

        }
    }
}
