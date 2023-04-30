using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Commands.CreateNewCarPage;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Commands.UpdateNewCarPage;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Queries.GetDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers
{
    /// <summary>
    /// Manager new car page
    /// </summary>
    [Route("api/v{version:apiVersion}/newcar-pages")]
    public class NewCarPageController : ApiControllerBase
    {
        public NewCarPageController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Create newcar page
        /// </summary>
        /// <param name="pageForCreation"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(new[] { PermissionGrant.AddNewCarPage })]
        public async Task<ActionResult<int>> Create([FromBody] NewCarPageForCreation pageForCreation)
        {
            var result = await Mediator.Send(new CreateNewCarPageCommand(CurrentUser.Id, pageForCreation));
            return Ok(result);
        }

        /// <summary>
        /// Update newcar page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageForUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [HasPermission(new[] { PermissionGrant.UpdateNewCarPage })]
        public async Task<ActionResult> Update(int id, [FromBody] NewCarPageForUpdate pageForUpdate)
        {
            await Mediator.Send(new UpdateNewCarPageCommand(id, pageForUpdate, CurrentUser.Id));
            return NoContent();
        }

        /// <summary>
        /// Filter newcar page and paging
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        [HttpGet]
        [HasPermission(new[] { PermissionGrant.ViewListNewCarPage })]
        public async Task<ActionResult<PagedList<NewCarPageFilterVm>>> Filter(
            [FromQuery] NewCarPageFilterParam param)
        {
            var result = await Mediator.Send(new FilterNewCarPageQuery(param));
            return Ok(result);
        }
        
        /// <summary>
        /// Get newcar page detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<NewCarPageDetailVm>> GetDetail(int id)
        {
            var result = await Mediator.Send(new GetNewCarPageDetailQuery(id));
            return Ok(result);
        }
    }
}
