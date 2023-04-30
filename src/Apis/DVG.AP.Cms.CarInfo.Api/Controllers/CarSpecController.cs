using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Delete;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.GetDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/v{version:apiVersion}/car-specs")]
public class CarSpecController : ApiControllerBase
{
    // GET

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public CarSpecController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Filter CarSpect
    /// </summary>
    /// <param name="param"></param>
    /// <returns>List CarInfo exists CarProperties</returns>
    [HttpGet("filter")]
    [HasPermission(new[] { PermissionGrant.SetCarSpec })]
    public async Task<ActionResult<PagedList<CarSpecFilterVm>>> Filter([FromQuery] CarSpecFilterParam param)
    {
        var result = await Mediator.Send(new FilterCarSpectQuery() { CarSpectFilterParam = param });
        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="creation"></param>
    /// <returns></returns>
    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HasPermission(new[] { PermissionGrant.SetCarSpec })]
    public async Task<ActionResult<int>> Create([FromBody] CarInfoPropertyListForCreation creation)
    {
        var result = await Mediator.Send(new CreateCarInfoPropertyValueCommand(CurrentUser.Id, creation));
        return Ok(result);
    }

    /// <summary>
    /// Update CarInfoPropertyValue
    /// </summary>
    /// <returns></returns>
    [HttpPut()]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HasPermission(new[] { PermissionGrant.SetCarSpec })]
    public async Task<ActionResult> Update([FromBody] CarInfoPropertyListForUpdate forUpdate)
    {
        var result = await Mediator.Send(new UpdateCarInfoPropertyValueCommand(userId: CurrentUser.Id, forUpdate));
        return NoContent();
    }

    /// <summary>
    /// Get detail CarInforPropertyValues by CarInfoId
    /// </summary>
    /// <param name="carInfoId">The Id of CarInfo (Variant)</param>
    /// <param name="year">Year public</param>
    /// <returns></returns>
    [HttpGet("{carInfoId}")]
    public async Task<ActionResult<CarInfoGetDetailVm>> GetDetail(string carInfoId)
    {
        var result = await Mediator.Send(new GetCarInfoPropertyValueDetailQuery(carInfoId));
        return Ok(result);
    }

    /// <summary>
    /// Delete CarInfoPropertyValue by CarInfoId 
    /// </summary>
    /// <returns>ActionResult no content</returns>
    /// <response code="204"> Return delete success with no content</response>
    [HttpDelete("{carInfoId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [HasPermission(new[] { PermissionGrant.SetCarSpec })]
    public async Task<ActionResult> Delete(string carInfoId)
    {
        await Mediator.Send(new DeleteCarInfoPropertyValueCommand(carInfoId: carInfoId));
        return NoContent();
    }
}