using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Model.NewCarArticle.Strategies.GoogleAMP
{
    public interface IGoogleAMPStrategy
    {
        string ConvertContentToAMP(string? content);
    }
}
