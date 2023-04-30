using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("NewCarArticles")]
    public class NewCarArticle
    {
        [Key] public long Id { get; set; }

        /// <summary>
        /// If type = 1(brand) =&gt; objectId = brandId
        /// If type = 2(model) =&gt; objectId = ModelId
        /// If type = 3(carInfo) =&gt; objectId = CarInfoId
        /// </summary>
        public long ObjectId { get; set; }

        /// <summary>
        /// =1: Brand, =2: Brand model, =3:Brand Model Variant
        /// </summary>
        public NewCarArticleType Type { get; set; }

        /// <summary>
        /// Parent của objectId, 
        /// -Nếu bài newCar của variant (type = 3), thì objectId = carInfoId, đồng thời ParentOfObjectId sẽ tương đương với modelId của carInfo
        /// -Nếu newCarModel(type = 2) , thì objectId = modelId, đồng thời ParentOfObjectId sẽ tương đương với brandId của model tương ứng
        /// </summary>
        public long ParentOfObjectId { get; set; }

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
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }

        public string? LabelDisplay { get; set; }

        public virtual IEnumerable<ContentDetail>? Contents { get; set; }
        public virtual IEnumerable<CarImage>? Images { get; set; }
    }
}