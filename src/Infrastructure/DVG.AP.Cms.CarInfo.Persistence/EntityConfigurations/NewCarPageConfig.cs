using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class NewCarPageConfig : IEntityTypeConfiguration<NewCarPage>
    {
        public void Configure(EntityTypeBuilder<NewCarPage> entity)
        {
            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"NewCarPages_Id_seq\"'::regclass)");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("now()");

            entity.Property(e => e.Description).HasColumnType("character varying");

            entity.Property(e => e.IsHot).HasComment("True: hiển thị ở box footer");

            entity.Property(e => e.LastModifiedDate).HasColumnType("timestamp without time zone");

            entity.Property(e => e.MaxPrice).HasComment("Giá đến (cho loại Giá xe)");

            entity.Property(e => e.MinPrice).HasComment("Giá từ (cho loại Giá xe)");

            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasComment("VD: Dưới 400 tr\r\nTừ 400 tr-800tr");

            entity.Property(e => e.ObjectId)
                .HasComment(
                    "Mapping với Id của bảng khác. VD với page typye là 3: Theo hãng thì phải mapping với brandId, Theo kiểu dáng thì mapping với bodytype");

            entity.Property(e => e.Ordinal)
                .HasDefaultValueSql("0")
                .HasComment("Thứ tự hiển thị");

            entity.Property(e => e.Slug).HasMaxLength(256);

            entity.Property(e => e.Status).HasComment("Active status: 1-Active, 2-Inactive");

            entity.Property(e => e.Title)
                .HasMaxLength(256)
                .HasComment("VD: Khuyến mại xe dưới 500 triệu");

            entity.Property(e => e.Type)
                .HasComment("Loại trang: 1: Theo khoảng giá 2:Theo kiểu dáng 3:Theo hãng");
        }
    }
}
