using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments.Commands.Delete
{
    public class DeleteSegmentCommand : IRequest
    {
        public DeleteSegmentCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
