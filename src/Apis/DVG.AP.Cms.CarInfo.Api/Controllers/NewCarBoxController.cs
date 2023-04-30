using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Commands.SetDetails;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetAll;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetBoxSettingDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers
{
    [Route("api/v{version:apiVersion}/newcar-box")]
    public class NewCarBoxController : ApiControllerBase
    {
        public NewCarBoxController(IMediator mediator) : base(mediator)
        {
        }
        /// <summary>
        /// Get all newcar box
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IReadOnlyList<NewCarBoxVm>> GetAll()
        {
            return await Mediator.Send(new GetAllQuery());
        }

        /// <summary>
        /// Add or remove newcararticle into/out of NewCarBox
        /// </summary>
        /// <param name="newCarBoxSetDetail"></param>
        /// <returns></returns>
        [HttpPost("{newCarBoxId:int}/set-details")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HasPermission(new[] { PermissionGrant.SetNewCarArticleInBox })]
        public async Task<ActionResult<string>> SetSetails(int newCarBoxId, [FromBody] List<NewCarBoxDetailItem> newCarBoxSetDetail)
        {
            var result = await Mediator.Send(new SetDetailsCommand(new NewCarBoxSetDetail()
            {
                NewCarBoxId = newCarBoxId,
                Items = newCarBoxSetDetail
            }, CurrentUser.Id));
            return Ok(result.ToString());
        }

        /// <summary>
        /// Get NewCarBox setting detail by newCarBoxId
        /// </summary>
        /// <param name="newCarBoxId"></param>
        /// <returns></returns>
        [HttpGet("box-setting-detail/{newCarBoxId:int}")]
        [HasPermission(new[] { PermissionGrant.SetNewCarArticleInBox })]
        public async Task<NewCarBoxSettingDetailVm> GetBoxSettingDetail(int newCarBoxId)
        {
            return await Mediator.Send(new GetBoxSettingDetailQuery(newCarBoxId));
        }
    }
}
