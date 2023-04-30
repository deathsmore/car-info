using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Commands.CarColorInsert
{
    public class CarColorInsertCommand : IRequest<int>
    {
        public CarColorInsertCommand(CarColorInsert carColorInsert, int userId)
        {
            CarColorInsert = carColorInsert;
            CarColorInsert.Init(userId);
        }
        public CarColorInsert CarColorInsert { get; set; }
    }


    public class CarColorInsert
    {
        public void Init(int userId)
        {
            CreatedDate = DateTime.Now;
            CreatedBy = userId;
        }
        public string Name { get; set; } = String.Empty;
        public string Code { get; set; } = String.Empty;
        public DateTime CreatedDate { get; private set; }
        public int CreatedBy { get; private set; }
    }
}
