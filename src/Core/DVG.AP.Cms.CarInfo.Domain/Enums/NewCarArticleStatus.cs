namespace DVG.AP.Cms.CarInfo.Domain.Enums;

public enum NewCarArticleStatus : short
{
    Default = 0,
    WaitToEdit = 1,
    Draft = 2,
    Disapproved = 3,
    Approved = 4,
    WaitToApproved = 5,
    RequestToEdit = 6
}