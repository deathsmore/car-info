using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.Segments.Commands.CreateSegment;
using DVG.AP.Cms.CarInfo.Application.Features.Segments.Commands.UpdateSegment;
using DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.GetSegmentDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SegmentForCreation, Domain.Entities.Segment>();
            CreateMap<Domain.Entities.Segment, SegmentDetailVm>();
            CreateMap<SegmentForUpdate, Domain.Entities.Segment>();
            //CreateMap<Domain.Entities.Variant, VariantDetailVm>();
        }
    }
}
