using DVG.AP.Cms.CarInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    public class NewCarArticleBase
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? Sapo { get; set; }

        /// <summary>
        /// Ngày xuất bản
        /// </summary>
        public DateTime? PublishedDate { get; set; }

        public int PublishedBy { get; set; }
        public int SourceId { get; set; }
        public int AuthorId { get; set; }
        public bool IsDCMA { get; set; }
        public bool IsAMP { get; set; }
        public bool IsHot { get; set; }
        public bool IsFBInstantArticle { get; set; }
        public bool HaveVideo { get; set; }
        public string? VideoUrl { get; set; }
        public string? BrochureFile { get; set; }
        public bool IsOwnerReview { get; set; }
        public bool IsCompare { get; set; }
        public bool IsImage360 { get; set; }
        public double Rate { get; set; }
        public bool IsFAQs { get; set; }

        /// <summary>
        /// Ngày publish, để dánh index =&gt; filter
        /// </summary>
        public long PublishedDateSpan { get; set; }

        /// <summary>
        /// Có show màu xe hay không
        /// </summary>
        public bool IsShowColorImages { get; set; }

        /// <summary>
        /// 1 - WaitToEdit: Phóng viên, CTV tạo bài xong gửi chờ biên tập
        /// 2 - Draft: Bài viết đang lưu nháp
        /// 3 - Disapproved: quản lý biên tập ko duyệt bài viết của biên tập hoặc ctv
        /// 4 - Approved: Quản lý biên tập đã duyệt bài của biên tập, ctv, pv và publish lên trang
        /// 5 - WaitToApproved: chờ xuất bản từ ql biên tập
        /// 6 - RequestToEdit: yêu cầu chỉnh sửa từ ql biên tập
        /// </summary>
        public NewCarArticleStatus Status { get; set; }

        public string? Avatar { get; set; }
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// -1: dữ liệu crawl
        /// </summary>
        public int CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string? LabelDisplay { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public int TotalPromotion { get; set; }

        //Navigation properties
        public virtual IEnumerable<CarImage>? Images { get; set; }
        public virtual IEnumerable<ContentDetail>? Contents { get; set; }
        public virtual IEnumerable<NewCarFAQs>? NewCarFAQs  { get; set; }
        public virtual NewCarSEOInfos? NewCarSEOInfos { get; set; }


    }
}
