using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;

public class NewCarArticleFactory
{
    private readonly NewCarArticleBrand _newCarArticleBrand;
    private readonly NewCarArticleVariant _newCarArticleVariant;
    private readonly NewCarArticleModel _newCarArticleModel;
    private readonly NewCarArticleDefault _newCarArticleDefault;

   
    public NewCarArticleFactory(
        NewCarArticleBrand newCarArticleBrand,
        NewCarArticleVariant newCarArticleVariant,
        NewCarArticleModel newCarArticleModel,
        NewCarArticleDefault newCarArticleDefault)
    {
        _newCarArticleBrand = newCarArticleBrand;
        _newCarArticleVariant = newCarArticleVariant;
        _newCarArticleModel = newCarArticleModel;
        _newCarArticleDefault = newCarArticleDefault;
    }

    public NewCarArticleFactory() { }
    
    public virtual NewCarArticleAbstract CreateNewCarArticle(NewCarArticleType type)
    {
        return type switch
        {
            //NewCarArticleType.Default => _newCarArticleDefault,
            NewCarArticleType.Brand => _newCarArticleBrand,
            NewCarArticleType.Model => _newCarArticleModel,
            NewCarArticleType.Variant => _newCarArticleVariant,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}