using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Delete;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/v{version:apiVersion}/car-groups")]
public class CarPropertyGroupController : ApiControllerBase
{
    // GET
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public CarPropertyGroupController(IMediator mediator) : base(mediator)
    {
    }


    /// <summary>
    /// Filter CarPropertyGroup
    /// </summary>
    /// <returns></returns>
    [HttpGet("filter")]
    [HasPermission(new[] { PermissionGrant.ConfigSpecStructure })]
    public async Task<ActionResult<IReadOnlyList<CarPropertyGroupFilterVm>>> Filter(
        [FromQuery] CarPropertyGroupFilterParam param
    )
    {
        var result = await Mediator.Send(new FilterCarPropertyGroupQuery(param));
        return Ok(result);
    }

    /// <summary>
    /// Get All CarPropertyGroup include List CarProperty
    /// </summary>
    /// <returns>List CarPropertyGroup</returns>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CarPropertyGroupGetListVm>>> GetList()
    {
        var result = await Mediator.Send(new GetListCarGroupQuery());
        return Ok(result);
    }

    /// <summary>
    /// Get CarPropertyGroup Detail
    /// </summary>
    /// <param name="id"> The Id of CarPropertyGroup</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarPropertyGroupDetailVm>> GetDetail(int id)
    {
        var result = await Mediator.Send(new GetDetailCarPropertyGroupQuery(id));
        return Ok(result);
    }

    /// <summary>
    /// Create CarPropertyGroup and CarProperties
    /// </summary>
    /// <param name="creation"></param>
    /// <returns>The Id of CarPropertyGroup</returns>
    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HasPermission(new[] { PermissionGrant.ConfigSpecStructure })]
    public async Task<ActionResult<int>> Create([FromBody] CarPropertyGroupForCreation creation)
    {
        var result = await Mediator.Send(new CreateCarPropertyGroupCommand(CurrentUser.Id, creation));
        return Ok(result);
    }

    /// <summary>
    /// Update CarPropertyGroup and CarProperties
    /// </summary>
    /// <param name="id">The Id of CarPropertyGroup</param>
    /// <param name="forUpdate"></param>
    /// <returns>status code 204 update success</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HasPermission(new[] { PermissionGrant.ConfigSpecStructure })]
    public async Task<ActionResult> Update(int id, [FromBody] CarPropertyGroupForUpdate forUpdate)
    {
        await Mediator.Send(new UpdateCarPropertyGroupCommand(id: id, userId: CurrentUser.Id, forUpdate));
        return NoContent();
    }

    /// <summary>
    /// Delete CarPropertyGroup and CarProperties of it
    /// </summary>
    /// <param name="id">The Id of CarPropertyGroup</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [HasPermission(new[] { PermissionGrant.ConfigSpecStructure })]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCarPropertyGroupCommand(id));
        return NoContent();
    }
}