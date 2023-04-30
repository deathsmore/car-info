using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Commands.Update
{
    public class UpdateVariantCommand : IRequest<int>
    {
        public UpdateVariantCommand(int id, VariantForUpdate variant, int userId)
        {
            Variant = variant;
            Variant.Init(id, userId);

        }

        public VariantForUpdate Variant { get; set; }
    }
    public class VariantForUpdate
    {
        public void Init(int id, int userId)
        {
            Id = id;
            ModifiedDate = DateTime.Now;
            ModifiedBy = userId;
        }
        public int Id { get; private set; }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public bool IsVirtual { get; set; }
        public int ModifiedBy { get; private set; }
        public ActiveStatus Status { get; set; }
        public DateTime? ModifiedDate { get; private set; }
    }
}
