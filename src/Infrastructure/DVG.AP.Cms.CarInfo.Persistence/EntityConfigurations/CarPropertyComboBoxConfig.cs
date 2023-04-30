using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CarPropertyComboBoxConfig  : IEntityTypeConfiguration<CarPropertyComboBox>
    {
        public void Configure(EntityTypeBuilder<CarPropertyComboBox> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("nextval('\"CarPropertyComboboxes_Id_seq\"'::regclass)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("now()");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

   
        }
    }
}