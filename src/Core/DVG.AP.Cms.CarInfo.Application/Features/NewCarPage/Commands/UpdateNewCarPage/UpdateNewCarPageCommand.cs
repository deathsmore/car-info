using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Commands.UpdateNewCarPage
{
    public class UpdateNewCarPageCommand : IRequest<Unit>
    {
        public UpdateNewCarPageCommand(int newCarPageId, NewCarPageForUpdate newCarPageForUpdate,
            int userId)
        {
            BadRequestException.BadRequestArgument(newCarPageForUpdate, $"{nameof(NewCarPageForUpdate)} is null");
            NewCarPageForUpdate = newCarPageForUpdate;
            NewCarPageForUpdate.Init(userId, newCarPageId);
        }

        public NewCarPageForUpdate NewCarPageForUpdate { get; private set; }
    }

    public class NewCarPageForUpdate
    {
        public void Init(int userId, int id)
        {
            LastModifiedDate = DateTime.Now;
            LastModifiedBy = userId;
            Id = id;
        }

        public int Id { get; private set; }
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
        public DateTime? LastModifiedDate { get; private set; }
        public int LastModifiedBy { get; private set; }
    }
}
