using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations;

public class NewCarBoxDetailConfiguration : IEntityTypeConfiguration<NewCarBoxDetail>
{
    public void Configure(EntityTypeBuilder<NewCarBoxDetail> entity)
    {
        entity.ToTable("NewCarBoxDetails");

        entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"CarInfoBoxDetail_Id_seq\"'::regclass)");

        entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

        entity.Property(e => e.ObjectId).HasComment("ObjectId = BrandId | ModelId | CarInfoId. dependence ObjectType field");

        entity.Property(e => e.ObjectType).HasComment("ObjectType = 1: Brand, 2: Model, 3: Variant");
    }
}