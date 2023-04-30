using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.GetSegmentDetail
{
    public class GetSegmentDetailQuery : IRequest<SegmentDetailVm>
    {
        public int Id { get; set; }
    }
    public class SegmentDetailVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
        public List<ModelInSegmentVm> Models { get; set; } = new List<ModelInSegmentVm>();
    }
    public class ModelInSegmentVm
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
    }
}
