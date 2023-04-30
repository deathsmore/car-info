using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CarInfoPropertyValueConfig  : IEntityTypeConfiguration<CarInfoPropertyValue>
    {
        public void Configure(EntityTypeBuilder<CarInfoPropertyValue> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("nextval('\"CarPropertyValues_Id_seq\"'::regclass)");

            builder.Property(e => e.CarPropertyComboBoxId).HasComment("id of combobox of property if choice. Support for get list variant of model");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            builder.Property(e => e.LastModifiedDate).HasDefaultValueSql("now()");

            builder.Property(e => e.Value).HasMaxLength(200); 
        }
    }
}