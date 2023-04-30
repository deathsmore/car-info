using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Api.Infrastructure.Services;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Commands.CarColorInsert;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Commands.CarColorUpdate;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.GetColors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers
{

    [Route("api/v{version:apiVersion}/car-color")]
    public class CarColorController : ApiControllerBase
    {
        public int CurrentUserId;

        ///
        public CarColorController(IMediator mediator, IIdentityService identityService) : base(mediator)
        {
            CurrentUserId = identityService.GetUserIdentity();
        }

        /// <summary>
        /// Get list filter car color for management
        /// </summary>
        /// <param name="carColorParameter"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [HasPermission(new[] { PermissionGrant.ViewListColor })]
        public async Task<ActionResult<PagedList<FilterCarColorVm>>> Filter([FromQuery] FilterCarColorParameter carColorParameter)
        {
            return Ok(await Mediator.Send(new FilterCarColorQuery { CarColorParameter = carColorParameter }));
        }

        /// <summary>
        /// Get all list car colors
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<ActionResult<IReadOnlyList<GetAllCarColorVm>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCarColorQuery { }));
        }

        /// <summary>
        /// Add new car color
        /// </summary>
        /// <param name="carColorInsert"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(new[] { PermissionGrant.AddColor })]
        public async Task<ActionResult<int>> Create([FromBody] CarColorInsert carColorInsert)
        {
            return Ok(await Mediator.Send(new CarColorInsertCommand(carColorInsert, CurrentUser.Id)));
        }

        /// <summary>
        /// Update car color
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carColorUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [HasPermission(new[] { PermissionGrant.UpdateColor })]
        public async Task<ActionResult> Update(int id, [FromBody] CarColorUpdate carColorUpdate)
        {
            await Mediator.Send(new CarColorUpdateCommand(id, carColorUpdate, CurrentUser.Id));
            return NoContent();
        }
    }
}
