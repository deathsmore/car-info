using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class NewCarBrandConfiguration : NewCarArticleBaseConfiguration<NewCarBrand>
    {
        public override void Configure(EntityTypeBuilder<NewCarBrand> entity)
        {
            base.Configure(entity);
            entity.ToTable("NewCarBrands");
        }
    }
}
