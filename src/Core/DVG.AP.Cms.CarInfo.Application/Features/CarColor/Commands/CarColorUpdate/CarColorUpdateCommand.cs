using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Commands.CarColorUpdate
{
    public class CarColorUpdateCommand : IRequest<Unit>
    {
        public CarColorUpdateCommand(int id, CarColorUpdate carColorUpdate, int userId)
        {
            CarColorUpdate = carColorUpdate;
            CarColorUpdate.Init(id, userId);
        }
        public CarColorUpdate CarColorUpdate { get; set; }
    }

    public class CarColorUpdate
    {
        public void Init(int id, int userId)
        {
            Id = id;
            ModifiedDate = DateTime.Now;
            ModifiedBy = userId;
        }
        public int Id { get; private set; }
        public string Name { get; set; } = String.Empty;
        public string Code { get; set; } = String.Empty;
        public int ModifiedBy { get; private set; }
        public DateTime ModifiedDate { get; private set; }
    }
}
