using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations;


public abstract class NewCarArticleBaseConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : NewCarArticleBase
{
    public virtual void Configure(EntityTypeBuilder<TBase> entity)
    {
        
        entity.HasKey("Id");

        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.Property(e => e.Avatar).HasMaxLength(256);

        entity.Property(e => e.BrochureFile).HasMaxLength(500);

        entity.Property(e => e.IsAMP).HasColumnName("IsAMP");

        entity.Property(e => e.IsHot).HasColumnName("IsHot");

        entity.Property(e => e.IsDCMA).HasColumnName("IsDCMA");

        entity.Property(e => e.IsFAQs).HasColumnName("IsFAQs");

        entity.Property(e => e.IsFBInstantArticle).HasColumnName("IsFBInstantArticle");

        entity.Property(e => e.IsShowColorImages)
            .HasDefaultValueSql("false")
            .HasComment("Có show màu xe hay không");

        entity.Property(e => e.PublishedDate)
            .HasColumnType("timestamp without time zone")
            .HasComment("Ngày xuất bản");

        entity.Property(e => e.PublishedDateSpan).HasComment("Ngày publish, để dánh index => filter");

        entity.Property(e => e.Sapo).HasMaxLength(1000);

        entity.Property(e => e.Status).HasComment(
            "1 - WaitToEdit: Phóng viên, CTV tạo bài xong gửi chờ biên tập\r\n2 - Draft: Bài viết đang lưu nháp\r\n3 - Disapproved: quản lý biên tập ko duyệt bài viết của biên tập hoặc ctv\r\n4 - Approved: Quản lý biên tập đã duyệt bài của biên tập, ctv, pv và publish lên trang\r\n5 - WaitToApproved: chờ xuất bản từ ql biên tập\r\n6 - RequestToEdit: yêu cầu chỉnh sửa từ ql biên tập");

        entity.Property(e => e.Title).HasMaxLength(256);


        entity.Property(e => e.Url).HasMaxLength(256);

        entity.Property(e => e.VideoUrl).HasMaxLength(256);

        entity.HasMany<CarImage>(ci => ci.Images)
            .WithOne()
            .HasForeignKey(ci => ci.ObjectId);

        entity.HasMany<ContentDetail>(cd => cd.Contents)
            .WithOne()
            .HasForeignKey(cd => cd.NewCarArticleId);
        
        entity.HasMany<NewCarFAQs>(cd => cd.NewCarFAQs)
            .WithOne()
            .HasForeignKey(cd => cd.NewCarArticleId);

        entity.HasOne<NewCarSEOInfos>(si => si.NewCarSEOInfos)
            .WithOne()
            .HasForeignKey<NewCarSEOInfos>(si => si.NewCarArticleId);
    }
}

public class NewCarArticleConfiguration : IEntityTypeConfiguration<NewCarArticle>
{
    public void Configure(EntityTypeBuilder<NewCarArticle> entity)
    {
        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.Property(e => e.Avatar).HasMaxLength(256);

        entity.Property(e => e.BrochureFile).HasMaxLength(500);

        entity.Property(e => e.CreatedBy).HasComment("-1: dữ liệu crawl");

        entity.Property(e => e.CreatedDate)
            .HasColumnType("timestamp without time zone")
            .HasDefaultValueSql("now()");

        entity.Property(e => e.IsAMP).HasColumnName("IsAMP");

        entity.Property(e => e.IsHot).HasColumnName("IsHot");

        entity.Property(e => e.IsDCMA).HasColumnName("IsDCMA");

        entity.Property(e => e.IsFAQs).HasColumnName("IsFAQs");

        entity.Property(e => e.IsFBInstantArticle).HasColumnName("IsFBInstantArticle");

        entity.Property(e => e.IsShowColorImages)
            .HasDefaultValueSql("false")
            .HasComment("Có show màu xe hay không");

        entity.Property(e => e.ModifiedDate)
            .HasColumnType("timestamp without time zone")
            .HasDefaultValueSql("now()");

        entity.Property(e => e.ObjectId)
            .HasComment(
                "If type = 1(brand) => objectId = brandId\r\nIf type = 2(model) => objectId = ModelId\r\nIf type = 3(carInfo) => objectId = CarInfoId");

        entity.Property(e => e.ParentOfObjectId).HasComment(
            "Parent của objectId, \r\n-Nếu bài newCar của variant (type = 3), thì objectId = carInfoId, đồng thời ParentOfObjectId sẽ tương đương với modelId của carInfo\r\n-Nếu newCarModel(type = 2) , thì objectId = modelId, đồng thời ParentOfObjectId sẽ tương đương với brandId của model tương ứng");

        entity.Property(e => e.PublishedDate)
            .HasColumnType("timestamp without time zone")
            .HasComment("Ngày xuất bản");

        entity.Property(e => e.PublishedDateSpan).HasComment("Ngày publish, để dánh index => filter");

        entity.Property(e => e.Sapo).HasMaxLength(1000);

        entity.Property(e => e.Status).HasComment(
            "1 - WaitToEdit: Phóng viên, CTV tạo bài xong gửi chờ biên tập\r\n2 - Draft: Bài viết đang lưu nháp\r\n3 - Disapproved: quản lý biên tập ko duyệt bài viết của biên tập hoặc ctv\r\n4 - Approved: Quản lý biên tập đã duyệt bài của biên tập, ctv, pv và publish lên trang\r\n5 - WaitToApproved: chờ xuất bản từ ql biên tập\r\n6 - RequestToEdit: yêu cầu chỉnh sửa từ ql biên tập");

        entity.Property(e => e.Title).HasMaxLength(256);

        entity.Property(e => e.Type).HasComment("=1: Brand, =2: Brand model, =3:Brand Model Variant");

        entity.Property(e => e.Url).HasMaxLength(256);

        entity.Property(e => e.VideoUrl).HasMaxLength(256);
        entity.HasMany<CarImage>(ci => ci.Images)
            .WithOne()
            .HasForeignKey(ci => ci.ObjectId);

        entity.HasMany<ContentDetail>(cd => cd.Contents)
                .WithOne()
                .HasForeignKey(cd => cd.NewCarArticleId);
        
       
    }
}