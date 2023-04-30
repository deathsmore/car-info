namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.Filter
{
    public class FilterCarColorVm
    {
        public FilterCarColorVm(Domain.Entities.CarColor carColor)
        {
            Id = carColor.Id;
            Name = carColor.Name;
            Code = carColor.Code;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
