using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Queries.GetDetail
{
    public class GetNewCarPageDetailQuery : IRequest<NewCarPageDetailVm>
    {
        public GetNewCarPageDetailQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class NewCarPageDetailVm
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public NewCarPageType Type { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string? Name { get; set; }
        public ActiveStatus Status { get; set; }
        public short? Ordinal { get; set; }
        public bool? IsHot { get; set; }
        public int ObjectId { get; set; }
        public string? Slug { get; set; }
        public bool HasPromotion { get; set; }
    }
}
