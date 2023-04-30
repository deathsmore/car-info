using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CarPriceConfig  : IEntityTypeConfiguration<CarPrice>
    {
        public void Configure(EntityTypeBuilder<CarPrice> builder)
        {
            builder.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            builder.Property(e => e.CurrencyUnitId).HasDefaultValueSql("0");

            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("now()");

            builder.Property(e => e.OptionType).HasComment(@"Loại option của giá. VD: Giá theo màu xe
1: Color"); 
        }
    }
}