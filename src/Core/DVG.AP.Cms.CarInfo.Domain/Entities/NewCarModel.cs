using DVG.AP.Cms.CarInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    public class NewCarModel : NewCarArticleBase
    {
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public bool HasNewCarVariant { get; set; }
        public CarState CarState { get; set; }

        //Relations
        public Model Model { get; set; }
        //public virtual IEnumerable<ContentDetail>? Contents { get; set; }
    }
}
