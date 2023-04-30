using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetAll
{
    public class GetAllVariantQuery : IRequest<IReadOnlyList<VariantVm>>
    {
        public int ModelId { get; set; }
    }

    public class VariantVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public bool IsVirtual { get; set; }
    }
}
