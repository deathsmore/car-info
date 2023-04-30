// using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
// {
//     public class ObjectTypeConfig  : IEntityTypeConfiguration<ObjectType>
//     {
//         public void Configure(EntityTypeBuilder<ObjectType> builder)
//         {
//             builder.ToTable("ObjectType", "enum");
//
//             builder.HasComment("Danh sách các đối tượng trong hệ thống, để phân biệt trong các bảng dùng chung, VD: bảng SeoInfo lưu thông tin SEO của nhiều đối tượng Author, category, article....");
//
//             builder.Property(e => e.Id).ValueGeneratedNever();
//
//             builder.Property(e => e.Name).HasMaxLength(100); 
//         }
//     }
// }