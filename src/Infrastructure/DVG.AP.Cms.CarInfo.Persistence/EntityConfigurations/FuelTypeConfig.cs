using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class FuelTypeConfig   : IEntityTypeConfiguration<FuelType>
    {
        public void Configure(EntityTypeBuilder<FuelType> builder)
        {
            // builder.ToTable("FuelType", "enum");

            builder.HasComment("Danh sách các loại nhiên liệu");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Name).HasMaxLength(100); 
        }
    }
}
