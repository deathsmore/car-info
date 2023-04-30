using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class VariantConfig : IEntityTypeConfiguration<Domain.Entities.Variant>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Variant> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.IsVirtual).HasDefaultValueSql("false");
        }
    }
}
