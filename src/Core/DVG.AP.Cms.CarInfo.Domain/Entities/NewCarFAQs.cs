using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Domain.Entities;

public class NewCarFAQs
{
    public int Id { get; set; }
    public long NewCarArticleId { get; set; }
    public NewCarArticleType NewCarType { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }   
}