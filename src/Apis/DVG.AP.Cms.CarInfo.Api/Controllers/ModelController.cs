using DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllByConditions;
using DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllByConditions.Models;
using DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllForCRUDPromotion;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers
{
    /// <summary>
    /// Model controller
    /// </summary>
    [Route("api/v{version:apiVersion}/models")]
    public class ModelController : ApiControllerBase
    {
        public ModelController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Get all model by brand and status
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("brand/{brandId:int}")]
        public async Task<ActionResult<IReadOnlyList<ModelVm>>> GetModels(int brandId, [FromQuery] ActiveStatus status)
        {
            return Ok(await Mediator.Send(new GetAllQuery() { Status = status, BrandId = brandId }));
        }

        /// <summary>
        /// Get list model for create/update promotion
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("for-crud-promotion")]
        public async Task<ActionResult<IReadOnlyList<ModelVm>>> GetModelsForCRUDPromotion([FromQuery] int brandId, [FromQuery] ActiveStatus status)
        {
            return Ok(await Mediator.Send(new GetAllModelForCRUDPromotionQuery() { Status = status, BrandId = brandId }));
        }
    }
}
