using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
   [Table("NewCarArticleDetails")]
   public class ContentDetail
   {
        [Key]
        public int Id { get; set; }
        [Column("NewCarArticleId")] 
        public long NewCarArticleId { get; set; }
        public string? ContentTitle { get; set; }
        /// <summary>
        /// 1-Thường 2-highlight 3-ưu điểm 4- nhược điểm
        /// </summary>
        public NewCarDetailContentType ContentType { get; set; }
        public short Order { get; set; }
        public string? LongContent { get; set; }
        public string? GgAmpContent { get; set; }
        public string? AppContent { get; set; }
        public string? FbContent { get; set; }
        /// <summary>
        /// Cho phép ẩn/hiện nội dung
        /// </summary>
        public bool Visible { get; set; }
        public NewCarArticleType Type { get; set; }
    }
}
