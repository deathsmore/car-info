using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class NewCarSEOInfosConfig
        : IEntityTypeConfiguration<NewCarSEOInfos>
    {
        public void Configure(EntityTypeBuilder<NewCarSEOInfos> entity)
        {
            entity.Property(e => e.NewCarArticleId).HasComment("NewCarArticleId is foreignKey with NewCarArticle Table");

            entity.Property(e => e.Type).HasComment("Type is New Car for Brand = 1 , Model = 2, Variant = 3");

            entity.Property(e => e.MetaDescription).HasColumnType("character varying");

            entity.Property(e => e.MetaKeyword).HasColumnType("character varying");

            entity.Property(e => e.MetaTitle).HasMaxLength(500);

            entity.Property(e => e.SEOKeyword)
                .HasColumnName("SEOKeyword")
                .HasMaxLength(500)
                .HasComment("Keyword để search trong bài viết các keyword tương ứng, chuyển thành dạng anchor text");

            entity.Property(e => e.TitleSeo)
                .HasMaxLength(256)
                .HasComment("thay thế metatitle khi trường này trống, used in Phil");
        }
    }
}