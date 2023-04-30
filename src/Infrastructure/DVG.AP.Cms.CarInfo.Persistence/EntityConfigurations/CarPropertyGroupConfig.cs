using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CarPropertyGroupConfig  : IEntityTypeConfiguration<CarPropertyGroup>
    {
        public void Configure(EntityTypeBuilder<CarPropertyGroup> builder)
        {
            builder.HasIndex(e => new { e.Ordinal, e.Status })
                .HasName("carpropertygroups_ordinal_status_index");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            builder.Property(e => e.LastModifiedDate).HasDefaultValueSql("now()");

            builder.Property(e => e.Name).HasMaxLength(200); 
        }
    }
}