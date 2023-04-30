using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class BrandConfig
        : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Alias).HasMaxLength(256);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp(0) without time zone")
                .HasDefaultValueSql("now()");

            entity.Property(e => e.Logo).HasMaxLength(256);

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp(0) without time zone")
                .HasDefaultValueSql("now()");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}