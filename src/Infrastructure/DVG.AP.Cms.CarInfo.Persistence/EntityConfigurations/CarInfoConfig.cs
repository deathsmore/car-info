using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CarInfoConfig  : IEntityTypeConfiguration<Domain.Entities.CarInfo>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.CarInfo> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Avatar).HasMaxLength(500);

            builder.Property(e => e.Engine).HasMaxLength(256);

            builder.Property(e => e.IsDiscontinued).HasDefaultValueSql("false");


            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.Url).HasMaxLength(256); 
            builder.HasMany<CarImage>(ci => ci.Images)
                .WithOne()
                .HasForeignKey(ci => ci.ObjectId);
        }
    }
}