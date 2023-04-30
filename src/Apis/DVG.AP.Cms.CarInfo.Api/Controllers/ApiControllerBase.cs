using DVG.AP.Cms.CarInfo.Api.Authorization;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DVG.AP.Cms.CarInfo.Api.Controllers;

/// <summary>
/// Api controller base
/// </summary>
[Produces("application/json", "application/xml")]
[ApiController]
[Authorize]
public class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    protected readonly IMediator Mediator;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pagedList"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected ActionResult ResponseList<T>(PagedList<T> pagedList) where T : class
    {
        if (!pagedList.Collections.Any()) return NoContent();
        return Ok(pagedList);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pagedList"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected ActionResult ResponseList<T>(IReadOnlyList<T> pagedList) where T : class
    {
        if (!pagedList.Any()) return NoContent();
        return Ok(pagedList);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected ActionResult ResponseObject<T>(T? input) where T : class
    {
        if (input == null) return NotFound();
        return Ok(input);
    }

    public ActivatingUser CurrentUser
    {
        get
        {
            if (User == null)
            {
                return null;
            }
            var userId = User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;// "sub"
            var userName = User.FindFirst(o => o.Type == ClaimTypes.Name)?.Value;

            return new ActivatingUser()
            {
                Id = int.Parse(userId),
                UserName = userName
            };
        }
    }
    public ApiControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }
}