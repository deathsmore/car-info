using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    public class NewCarBrand : NewCarArticleBase
    {
        public int BrandId { get; set; }
        public bool HasNewCarModel { get; set; }

        //Relations
        public Brand Brand { get; set; }
        //public virtual IEnumerable<ContentDetail>? Contents { get; set; }
    }
}
