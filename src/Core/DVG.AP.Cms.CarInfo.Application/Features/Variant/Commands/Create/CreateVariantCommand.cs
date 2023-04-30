using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Commands.Create
{
    public class CreateVariantCommand : IRequest<int>
    {
        public CreateVariantCommand()
        {

        }

        public CreateVariantCommand(VariantForCreation variant, int userId)
        {
            Variant = variant;
            Variant.Init(userId);
        }
        public VariantForCreation Variant { get; set; }
    }

    public class VariantForCreation
    {
        public void Init(int userId)
        {
            CreatedDate = DateTime.Now;
            CreatedBy = userId;
        }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public bool IsVirtual { get; set; }
        public int CreatedBy { get; private set; }
        public ActiveStatus Status { get; set; }
        public DateTime CreatedDate { get; private set; }
    }
}
