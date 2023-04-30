using DVG.AP.Cms.CarInfo.Application.Features.BodyType.Models;
using DVG.AP.Cms.CarInfo.Application.Features.BodyType.Queries.GetAllByCondition;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/body-type")]
    public class BodyTypeController : ApiControllerBase
    {
        public BodyTypeController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Get list body type
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BodyTypeVm>>> GetAll([FromQuery] GetAllByConditionsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
