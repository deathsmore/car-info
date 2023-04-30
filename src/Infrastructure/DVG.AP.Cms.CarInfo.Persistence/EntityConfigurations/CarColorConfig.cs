using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CarColorConfig  : IEntityTypeConfiguration<CarColor>
    {
        public void Configure(EntityTypeBuilder<CarColor> builder)
        {

            builder.Property(e => e.Id).HasDefaultValueSql("nextval(('public.newcarcolors_id_seq'::text)::regclass)");
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.Name).HasMaxLength(256);
        }
    }
}