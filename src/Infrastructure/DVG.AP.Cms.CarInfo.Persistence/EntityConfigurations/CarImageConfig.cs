using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CarImageConfig : IEntityTypeConfiguration<CarImage>
    {
        public void Configure(EntityTypeBuilder<CarImage> builder)
        {
            builder.HasIndex(e => new { ObjectId = e.ObjectId, e.Ordinal })
                .HasName("newcarimages_newcarid_ordinal_index");

            builder.Property(e => e.Id).HasDefaultValueSql("nextval(('public.newcarimages_id_seq'::text)::regclass)");

            builder.Property(e => e.AltText).HasMaxLength(256);

            builder.Property(e => e.Title).HasMaxLength(500);

            builder.Property(e => e.Type).HasComment("1-Exterior,2- Interior,3- Feature");

            builder.Property(e => e.Url).HasMaxLength(1000);
        }
    }
}