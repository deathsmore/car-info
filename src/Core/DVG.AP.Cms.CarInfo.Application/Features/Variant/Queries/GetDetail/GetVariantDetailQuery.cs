using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetDetail
{
    public class GetVariantDetailQuery: IRequest<VariantDetailVm>
    {
        public int Id { get; set; }
    }
    public class VariantDetailVm
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string Name { get; set; }
        public bool IsVirtual { get; set; }
        public ActiveStatus Status { get; set; }
    }
}
