using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class BodyTypeConfig : IEntityTypeConfiguration<BodyType>
    {
        public void Configure(EntityTypeBuilder<BodyType> builder)
        {
            builder.HasIndex(e => new {e.Ordinal, e.Name})
                .HasName("bodytypes_ordinal_name_index");

            builder.HasIndex(e => new {e.Ordinal, e.Name, e.Status})
                .HasName("bodytypes_odinal_name_status_index");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.Status).HasComment("Active status: 1-Active, 2-Inactive");
        }
    }
}