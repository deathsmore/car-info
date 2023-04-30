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
    
    public class NewCarModelConfiguration :NewCarArticleBaseConfiguration<NewCarModel>  
    {
        public override void Configure(EntityTypeBuilder<NewCarModel> entity)
        {
            base.Configure(entity);
            entity.ToTable("NewCarModels");
        }
    }
}
