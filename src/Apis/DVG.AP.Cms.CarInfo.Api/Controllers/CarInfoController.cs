using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Api.Infrastructure.Services;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoInsert;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoUpdate;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetCarInfos;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetInfoForCreatePromotion;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers
{

    [Route("api/v{version:apiVersion}/car-info")]
    [Authorize]
    public class CarInfoController : ApiControllerBase
    {
        public CarInfoController(IMediator mediator, IIdentityService identityService) : base(mediator)
        {
        }

        /// <summary>
        /// Get car info by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CarInfoGetDetailVm>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetCarInfoDetailQuery { Id = id.ToLong() }));
        }

        /// <summary>
        /// Listing Car info for manager
        /// </summary>
        /// <param name="carInfoParameter"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [HasPermission(new[] { PermissionGrant.ViewListCarInfo })]
        public async Task<ActionResult<PagedList<FilterCarInfoVm>>> Filter([FromQuery] FilterCarInfoParameter carInfoParameter)
        {
            return Ok(await Mediator.Send(new FilterCarInfoQuery { CarInfoParameter = carInfoParameter }));
        }

        /// <summary>
        /// get list car info for combobox
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("{variantId:int}/carinfos")]
        public async Task<ActionResult<IReadOnlyList<CarInfoVm>>> GetCarInfos(int variantId, [FromQuery] ActiveStatus status)
        {
            return Ok(await Mediator.Send(new GetCarInfoByModelAndStatusQuery { Status = status, VariantId = variantId }));
        }

        /// <summary>
        ///  Create car info with car prices, car images
        /// </summary>
        /// <param name="carInfoInsert"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(new[] { PermissionGrant.AddCarInfo })]
        public async Task<ActionResult<int>> Create([FromBody] CarInfoInsert carInfoInsert)
        {
            return Ok(await Mediator.Send(new CarInfoInsertCommand(carInfoInsert, CurrentUser.Id)));
        }

        /// <summary>
        /// Update car info with car prices, car images
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carInfoUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [HasPermission(new[] { PermissionGrant.UpdateCarInfo })]
        public async Task<ActionResult> Update(string id, [FromBody] CarInfoUpdate carInfoUpdate)
        {
            await Mediator.Send(new CarInfoUpdateCommand(id.ToLong(), carInfoUpdate, CurrentUser.Id));
            return NoContent();
        }

        /// <summary>
        /// Get car info for create promotion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("info-for-create-promotion")]
        public async Task<ActionResult<CarInfoForCreatePromotionVm>> GetInfoForCreatePromotion([FromQuery] string id)
        {
            return Ok(await Mediator.Send(new GetInfoForCreatePromotionQuery { CarInfoId = id.ToLong() }));
        }
    }
}
