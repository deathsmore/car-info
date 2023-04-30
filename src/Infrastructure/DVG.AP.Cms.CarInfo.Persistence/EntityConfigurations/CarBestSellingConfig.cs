using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CarBestSellingConfig:  IEntityTypeConfiguration<CarBestSelling>
    {
        public void Configure(EntityTypeBuilder<CarBestSelling> builder)
        {
        }
    }
}