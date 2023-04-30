using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CurrencyUnitConfig   : IEntityTypeConfiguration<CurrencyUnit>
    {
        public void Configure(EntityTypeBuilder<CurrencyUnit> builder)
        {
           
            builder.ToTable("CurrencyUnit", "enum");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Name).HasMaxLength(50);

            builder.Property(e => e.NameAlias).HasMaxLength(50); 
        }
    }
}