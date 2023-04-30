using DVG.AP.Cms.CarInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("NewCarPages")]
    public class NewCarPage
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// VD: Khuyến mại xe dưới 500 triệu
        /// </summary>
        public string? Title { get; set; }

        public string? Description { get; set; }

        /// <summary>
        /// Loại trang promtion: 1: Theo khoảng giá 2:Theo kiểu dáng 3:Theo hãng
        /// </summary>
        public NewCarPageType Type { get; set; }

        /// <summary>
        /// Giá từ (cho loại Giá xe)
        /// </summary>
        public double MinPrice { get; set; }

        /// <summary>
        /// Giá đến (cho loại Giá xe)
        /// </summary>
        public double MaxPrice { get; set; }

        /// <summary>
        /// VD: Dưới 400 tr
        /// Từ 400 tr-800tr
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public short Ordinal { get; set; }

        /// <summary>
        /// True: hiển thị ở box footer
        /// </summary>
        public bool? IsHot { get; set; }

        /// <summary>
        /// Mapping với Id của bảng khác. VD với page typye là 3: Theo hãng thì phải mapping với brandId, Theo kiểu dáng thì mapping với bodytype
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// Active status: 1-Active, 2-Inactive
        /// </summary>
        public ActiveStatus Status { get; set; }

        public string? Slug { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public bool HasPromotion { get; set; }
    }
}
