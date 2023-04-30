using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations
{
    public class CarPropertyConfig  : IEntityTypeConfiguration<CarProperty>
    {
        public void Configure(EntityTypeBuilder<CarProperty> builder)
        {
            builder.HasIndex(e => new { e.CarPropertyGroupId, e.Status })
                .HasName("carproperties_carpropertygroupid_status_index");

            builder.Property(e => e.CarPropertyComboBoxId).HasComment("id of table carpropertycombobox");

            builder.Property(e => e.DefaultValue).HasMaxLength(250);

            builder.Property(e => e.IsCrawled).HasComment("truong duoc craw tu anh Nhat");

            builder.Property(e => e.IsModelSpec).HasComment("danh dau truong show ra spec model");

            builder.Property(e => e.IsMultiChoice).HasComment("Ð?i tên t? ChoiceMulti sang IsMultiChoice");

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .HasComment("Ð?i tên t? title sang name");

            builder.Property(e => e.Status).HasComment("Active status: 1-Active, 2-Inactive");

            builder.Property(e => e.Type).HasComment(@"1 - textbox
2 - textbox number
3 - combobox
4 - checkbox
5 - datetimepicker"); 
        }
    }
}