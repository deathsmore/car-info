using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments.Commands.UpdateSegment
{
    public class UpdateSegmentCommand : IRequest<int>
    {
        public UpdateSegmentCommand()
        {

        }
        public UpdateSegmentCommand(int id, SegmentForUpdate segment, int userId)
        {
            Segment = segment;
            Segment.Init(id, userId);
        }
        public SegmentForUpdate Segment { get; set; }
    }

    public class SegmentForUpdate
    {
        public SegmentForUpdate()
        {

        }
        public void Init(int id, int userId)
        {
            Id = id;
            ModifiedDate = DateTime.Now;
            ModifiedBy = userId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
        public List<int> Models { get; set; }
        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
        [JsonIgnore]
        public int ModifiedBy { get; set; }
    }
}
