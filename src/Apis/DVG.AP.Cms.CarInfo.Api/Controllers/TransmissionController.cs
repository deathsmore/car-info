using DVG.AP.Cms.CarInfo.Application.Features.Transmission.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers
{
    [Route("api/v{version:apiVersion}/transmissions")]
    public class TransmissionController : ApiControllerBase
    {
        public TransmissionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TransmissionInListVm>>> GetAll([FromQuery] GetAllTransmissionQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
