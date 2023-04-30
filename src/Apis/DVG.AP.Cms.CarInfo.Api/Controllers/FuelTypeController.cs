using DVG.AP.Cms.CarInfo.Application.Features.FuelType.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers
{
    [Route("api/v{version:apiVersion}/fuel-types")]
    public class FuelTypeController : ApiControllerBase
    {
        public FuelTypeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<FuelTypeInListVm>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllFuelTypeQuery()));
        }
    }
}
