using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Api.Infrastructure.Services;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Delete;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetList;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetListUsingByCarSpec;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/v{version:apiVersion}/car-property-comboboxes")]
public class CarPropertyComboBoxController : ApiControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="identityService"></param>
    public CarPropertyComboBoxController(IMediator mediator, IIdentityService identityService) : base(mediator)
    {
    }

    /// <summary>
    /// Get list CarPropertyComboBox exist on CarProperty
    /// </summary>
    /// <returns>Collection CarPropertyComboBox</returns>
    [HttpGet("using-by-car-spec")]
    public async Task<ActionResult<IReadOnlyList<CarPropertyComboBoxGetListUsedVm>>> GetListUsingByCarSpec()
    {
        var result = await Mediator.Send(new GetListCarPropertyComboBoxUsedQuery());
        return Ok(result);
    }

    /// <summary>
    /// Get all CarPropertyComboBox not include child 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CarPropertyComboBoxGetListVm>>> GetList()
    {
        var result = await Mediator.Send(new GetListCarPropertyComboBoxQuery());
        return Ok(result);
    }

    /// <summary>
    /// Get CarPropertyComboBox Detail include CarPropertyComboBoxOption
    /// </summary>
    /// <param name="id"> The Id of CarPropertyComboBox</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarPropertyComboBoxDetail>> GetDetail(int id)
    {
        var result = await Mediator.Send(new GetDetailCarPropertyComboBoxQuery(id));
        return Ok(result);
    }

    /// <summary>
    /// filter CarPropertyComboBox
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [HttpGet("filter")]
    [HasPermission(new[] { PermissionGrant.ConfigSpecStructure })]
    public async Task<ActionResult<IReadOnlyList<CarPropertyComboBoxFilterVm>>> Filter(
        [FromQuery] CarPropertyComboBoxFilterParam param)
    {
        return Ok(await Mediator.Send(new FilterCarPropertyComboBoxQuery(param)));
    }

    /// <summary>
    /// Create CarPropertyComboBox
    /// </summary>
    /// <param name="creation"></param>
    /// <returns>The Id of CCarPropertyComboBo</returns>
    [HttpPost]
    [HasPermission(new[] { PermissionGrant.ConfigSpecStructure })]
    public async Task<ActionResult<int>> Create([FromBody] CarPropertyComboBoxForCreation creation)
    {
        var result = await Mediator.Send(new CarPropertyComboBoxCreateCommand(CurrentUser.Id, creation));
        return result;
    }

    /// <summary>
    /// Update CarPropertyComboBox and Child CarPropertyComboBoxOption
    /// </summary>
    /// <param name="id">The Id of CarPropertyComboBox</param>
    /// <param name="comboBoxForUpdateupdate"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HasPermission(new[] { PermissionGrant.ConfigSpecStructure })]
    public async Task<ActionResult> Update(int id, [FromBody] CarPropertyComboBoxForUpdate comboBoxForUpdateupdate)
    {
        await Mediator.Send(new CarPropertyComboBoxUpdateCommand(CurrentUser.Id, comboBoxForUpdateupdate, id: id));
        return NoContent();
    }

    /// <summary>
    /// Delete CarPropertyComboBox and all Child CarPropertyComboBoxOption 
    /// </summary>
    /// <param name="id">The Id of CarPropertyComboBox</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DelteCarPropertyComboBoxCommand(id));
        return NoContent();
    }
}