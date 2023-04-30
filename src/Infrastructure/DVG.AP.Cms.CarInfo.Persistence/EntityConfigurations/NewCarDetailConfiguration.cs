using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations;

public class NewCarDetailDetailConfiguration : IEntityTypeConfiguration<ContentDetail>
{
    public void Configure(EntityTypeBuilder<ContentDetail> entity)
    {
        entity.HasIndex(e => new { e.NewCarArticleId, e.Visible, e.Order }, "newcardetails_newcarid_visible_order_index");

        entity.Property(e => e.Id).HasDefaultValueSql("nextval(('public.newcardetails_id_seq'::text)::regclass)");

        entity.Property(e => e.AppContent).HasColumnType("character varying");

        entity.Property(e => e.ContentTitle).HasMaxLength(500);

        entity.Property(e => e.ContentType).HasComment("1-Thường 2-highlight 3-ưu điểm 4- nhược điểm");

        entity.Property(e => e.FbContent).HasColumnType("character varying");

        entity.Property(e => e.GgAmpContent).HasColumnType("character varying");

        entity.Property(e => e.LongContent).HasColumnType("character varying");

        entity.Property(e => e.Visible)
            .HasDefaultValueSql("true")
            .HasComment("Cho phép ẩn/hiện nội dung");
    }
}