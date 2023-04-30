using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.Model.NewCarArticle.Strategies.GoogleAMP;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ServiceLocators;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;


namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;

public abstract class NewCarArticleAbstract
{
    protected NewCarServiceLocator ServiceLocator;
    protected NewCarArticleType Type;
    public ObjectType ObjectType;
    protected NewCarArticleRestParam RestParam { get; private set; }
    public Domain.Entities.NewCarArticle NewCarArticle { get; private set; }

    public Domain.Entities.NewCarArticleBase NewCarEntity { get; protected set; }

    protected IGoogleAMPStrategy? GoogleAMPStrategy { get; private set; }

    public NewCarArticleAbstract(){}
    
    public NewCarArticleAbstract(NewCarServiceLocator serviceLocator)
    {
        ServiceLocator = serviceLocator;
        Type = NewCarArticleType.Default;
        RestParam = new NewCarArticleRestParam();
        var siteName = Environment.GetEnvironmentVariable("SITE_NAME");
        switch (siteName)
        {
            case "Philkotse":
                GoogleAMPStrategy = new PhilkotseGoogleAMPStrategy();
                break;
            default:
                GoogleAMPStrategy = null;
                break;
        }
        
    }

    /// <summary>
    /// Set Rest Parameters
    /// </summary>
    /// <param name="param"></param>
    public void SetRestParam(NewCarArticleRestParam param)
    {
        RestParam = param;
    }

   

    public abstract Task<PagedList<NewCarArticleFilterDto>> FilterAsync(NewCarArticleFilterParam filterParam);

    public virtual async Task<NewCarArticleBase?> Get(int id)
    {
        await Task.Delay(0);
        return null;
    }
    public virtual async Task<int> CountActiveArticles(int id)
    {
        await Task.Delay(0);
        return  0;
    }
    

    
    public virtual async Task<long> Insert(NewCarArticleForCreation newCarArticle)
    {
        Random rnd = new Random();
        long  result = rnd.Next(1, 1000);
        return result;
    }
    public virtual async Task<long> Update(NewCarArticleForUpdate newCarArticle)
    {
        return 1;
    }

    
    public virtual void SetNewCarEntity(Domain.Entities.NewCarArticleBase entity)
    {
        this.NewCarEntity = entity;
    }
    public virtual async Task<Domain.Entities.NewCarArticleBase> GetDetailAsync(long id)
    {
        return null!;
    }
    public abstract Task CalcPriceRange();
}

public class NewCarArticleRestParam
{
    public List<long> NewCarArticleIds { get; set; } = new List<long>();
}