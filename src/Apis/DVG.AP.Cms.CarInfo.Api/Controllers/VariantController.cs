using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Api.Infrastructure.Services;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetAll;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetAllForCRUDPromotion;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers
{
    [Route("api/v{version:apiVersion}/variants")]
    [Authorize]
    public class VariantController : ApiControllerBase
    {
        public int CurrentUserId;
        public VariantController(IMediator mediator, IIdentityService identityService) : base(mediator)
        {
            CurrentUserId = identityService.GetUserIdentity();
        }

        /// <summary>
        /// Get all variants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<VariantVm>>> GetList(int modelId)
        {
            var result = await Mediator.Send(new GetAllVariantQuery() { ModelId = modelId});
            return Ok(result);
        }

        /// <summary>
        /// Create variant
        /// </summary>
        /// <param name="creation"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(new[] { PermissionGrant.AddVariant })]
        public async Task<ActionResult<int>> Create([FromBody] VariantForCreation creation)
        {
            var result = await Mediator.Send(new CreateVariantCommand(creation, CurrentUser.Id));
            return result;
        }

        /// <summary>
        /// Filter list variant
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [HasPermission(new[] { PermissionGrant.ViewListVariant })]
        public async Task<ActionResult<PagedList<FilterVariantVm>>> Filter([FromQuery] FilterVariantParameter param)
        {
            return Ok(await Mediator.Send(new FilterVariantQuery { FilterVariantParameter = param }));
        }

        /// <summary>
        /// Get variant detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<VariantDetailVm>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetVariantDetailQuery { Id = id }));
        }

        /// <summary>
        /// Update variant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="variantUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [HasPermission(new[] { PermissionGrant.UpdateVariant })]
        public async Task<ActionResult> Update(int id, [FromBody] VariantForUpdate variantUpdate)
        {
            await Mediator.Send(new UpdateVariantCommand(id, variantUpdate, CurrentUser.Id));
            return NoContent();
        }


        /// <summary>
        /// Get list variant for create/update promotion
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        [HttpGet("for-crud-promotion")]
        public async Task<ActionResult<IReadOnlyList<VariantVm>>> GetModelsForCRUDPromotion(int modelId)
        {
            return Ok(await Mediator.Send(new GetAllVariantForCRUDPromotionQuery() { ModelId = modelId }));
        }
    }
}
