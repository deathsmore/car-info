using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CarPropertyComboboxOptionConfig : IEntityTypeConfiguration<CarPropertyComboboxOption>
    {
        public void Configure(EntityTypeBuilder<CarPropertyComboboxOption> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(256);
            builder.Property(e => e.ShortName).HasMaxLength(100);
        }
    }
}