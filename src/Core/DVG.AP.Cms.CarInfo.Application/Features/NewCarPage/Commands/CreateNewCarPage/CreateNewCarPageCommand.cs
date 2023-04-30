using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Commands.CreateNewCarPage
{
    public class CreateNewCarPageCommand : IRequest<int>
    {
        public CreateNewCarPageCommand(int userId, NewCarPageForCreation newCarPageForCreation)
        {
            NewCarPageForCreation = newCarPageForCreation;
            NewCarPageForCreation?.Init(userId);
        }

        public NewCarPageForCreation NewCarPageForCreation { get; set; }
    }

    public class NewCarPageForCreation
    {
        public void Init(int userId)
        {
            CreatedDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            CreatedBy = userId;
            LastModifiedBy = userId;
        }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public NewCarPageType Type { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string? Name { get; set; }
        public ActiveStatus Status { get; set; }
        public short? Ordinal { get; set; }
        public bool? IsHot { get; set; }
        public int ObjectId { get; set; }
        public string? Slug { get; set; }
        public bool HasPromotion { get; set; }

        public DateTime? CreatedDate { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTime? LastModifiedDate { get; private set; }
        public int LastModifiedBy { get; private set; }
    }
}
