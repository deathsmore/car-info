using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Api.Infrastructure.Services;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.Segments.Commands.CreateSegment;
using DVG.AP.Cms.CarInfo.Application.Features.Segments.Commands.UpdateSegment;
using DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.GetSegmentDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers
{
    [Route("api/v{version:apiVersion}/segments")]
    public class SegmentController : ApiControllerBase
    {
        public SegmentController(IMediator mediator, IIdentityService identityService) : base(mediator)
        {
        }

        /// <summary>
        /// Filter segments
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [HasPermission(new[] { PermissionGrant.ViewListSegment })]
        public async Task<ActionResult<PagedList<FilterSegmentVm>>> Filter([FromQuery] FilterSegmentParameter param)
        {
            return ResponseList(await Mediator.Send(new FilterSegmentQuery { FilterParams = param }));
        }

        /// <summary>
        /// Create Segment
        /// </summary>
        /// <param name="creation"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(new[] { PermissionGrant.AddSegment })]
        public async Task<ActionResult<int>> Create([FromBody] SegmentForCreation segment)
        {
            var result = await Mediator.Send(new CreateSegmentCommand(segment, CurrentUser.Id));
            return result;
        }

        /// <summary>
        /// Update Segment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="variantUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [HasPermission(new[] { PermissionGrant.UpdateSegment })]
        public async Task<ActionResult> Update(int id, [FromBody] SegmentForUpdate segment)
        {
            await Mediator.Send(new UpdateSegmentCommand(id, segment, CurrentUser.Id));
            return NoContent();
        }

        /// <summary>
        /// Get Segment detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SegmentDetailVm>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetSegmentDetailQuery { Id = id }));
        }
    }
}
