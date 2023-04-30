using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class ModelConfig : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> entity)
        {
            entity.HasIndex(e => new { e.BrandId, e.Status, e.Name })
                .HasName("models_brandid_isactive_modelname_index");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Alias).HasMaxLength(255);

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            entity.Property(e => e.IsShowListSearch)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasComment("Used in: Thai");

            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("now()");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}