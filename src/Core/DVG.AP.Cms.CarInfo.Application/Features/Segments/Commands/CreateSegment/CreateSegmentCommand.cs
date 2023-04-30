using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments.Commands.CreateSegment
{
    public class CreateSegmentCommand : IRequest<int>
    {
        public CreateSegmentCommand()
        {

        }
        public CreateSegmentCommand(SegmentForCreation segment, int userId)
        {
            Segment = segment;
            Segment.Init(userId);
        }
        public SegmentForCreation Segment { get; set; }
    }

    public class SegmentForCreation
    {
        public SegmentForCreation()
        {

        }
        public void Init(int userId)
        {
            CreatedDate = ModifiedDate = DateTime.Now;
            CreatedBy = ModifiedBy = userId;
        }
        public string Name { get; set; }
        public int? Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
        public List<int>? Models { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
        [JsonIgnore]
        public int ModifiedBy { get; set; }
    }
}
