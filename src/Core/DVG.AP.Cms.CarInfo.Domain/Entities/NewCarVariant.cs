using DVG.AP.Cms.CarInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    public class NewCarVariant : NewCarArticleBase
    {
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int VariantId { get; set; }
        public double Price { get; set; }
        public CarState CarState { get; set; }

        //Relations
        public Variant Variant { get; set; }
    }
}
