using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.CheckExist;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers;

/// <summary>
/// Manager NewCar Article
/// </summary>
[Route("api/v{version:apiVersion}/newcar-article")]
//[Authorize]
public class NewCarArticleController : ApiControllerBase
{
    // GET
    /// <summary>
    /// 
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public NewCarArticleController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Get NewCarArticle Detail
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<NewCarArticleGetDetailDto>> Get(string id, [FromQuery] NewCarArticleType type)
    {
        return Ok(await Mediator.Send(new GetDetailNewCarArticleQuery { Id = id.ToLong(), Type = type }));
    }

   

    /// <summary>
    /// Filter NewCarArticle
    /// </summary>
    /// <param name="param"></param>
    /// <returns>Collection NewCarArticle</returns>
    [HttpGet]
   // [HasPermission(new[] { PermissionGrant.ViewListNewCarArticle, PermissionGrant.SetNewCarArticleInBox })]
    public async Task<ActionResult> Filter([FromQuery] NewCarArticleFilterParam param)
    {
        var result = await Mediator.Send(new FilterNewCarArticleQuery(param));
        return ResponseList(result);
    }

    /// <summary>
    /// Create new car
    /// </summary>
    /// <param name="newCarForCreation"></param>
    /// <returns></returns>
    [HttpPost]
    [EnableCors("CorsPolicyCmsNewCars")]
    [HasPermission(new[] { PermissionGrant.AddNewCarArticle })]
    public async Task<ActionResult<string>> Create([FromBody] NewCarArticleForCreation newCarArticleForCreation)
    {
        var result = await Mediator.Send(new CreateNewCarArticleCommand(newCarArticleForCreation, CurrentUser.Id));
        return Ok(result.ToString());
    }

    /// <summary>
    /// Update new car
    /// </summary>
    /// <param name="id">The id of new car</param>
    /// <param name="newCarForUpdate"></param>
    [HttpPut("{id}")]
    [HasPermission(new[] { PermissionGrant.UpdateNewCarArticle })]
    public async Task<ActionResult> Update(string id, [FromBody] NewCarArticleForUpdate newCarForUpdate)
    {
        await Mediator.Send(new UpdateNewCarArticleCommand(newCarForUpdate, id.ToLong(), CurrentUser.Id));
        return NoContent();
    }

    /// <summary>
    /// Check exists by objectId and Type
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("check-exist")]
    public async Task<bool> CheckExists([FromQuery] CheckExistQuery query)
    {
        return await Mediator.Send(query);
    }
}