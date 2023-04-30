using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.Filter;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AP.Cms.CarInfo.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DVG.AP.Cms.CarInfo.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/weather-fore")]
public class WeatherForecastController : ControllerBase
{
    private readonly CarInfoDbContext _carInfoDbContext;
    private readonly IMediator Mediator;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, CarInfoDbContext carInfoDbContext, IMediator mediator)
    {
        _logger = logger;
        _carInfoDbContext = carInfoDbContext;
        Mediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("new-car")]
    public async Task<ActionResult<NewCarArticle>> GetNewCar()
    {
        var result = await _carInfoDbContext.NewCarArticles
            .Include(nc => nc.Images)
            .FirstOrDefaultAsync(nc => nc.Id == 68);


        return Ok(result);
        return Ok();
    }

    [HttpPut("new-car")]
    public async Task<ActionResult> CreateNewCar([FromBody] NewCarArticle newCarArticle)
    {
        _carInfoDbContext.NewCarArticles.Update(newCarArticle)
            ;
        await _carInfoDbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("new-car")]
    public async Task<ActionResult<int>> CreateNewcar([FromBody] NewCarArticle newCarArticle)
    {
        var result = await _carInfoDbContext.NewCarArticles.AddAsync(newCarArticle);
        var s = await _carInfoDbContext.SaveChangesAsync();
        return Ok(newCarArticle.Id);
    }
    /// <summary>
    /// Listing Car info for manager
    /// </summary>
    /// <param name="carInfoParameter"></param>
    /// <returns></returns>
    [HttpGet("carinfo-filter")]
    // [HasPermission(new[] { PermissionGrant.ViewListCarInfo })]
    public async Task<ActionResult<PagedList<FilterCarInfoVm>>> Filter([FromQuery] FilterCarInfoParameter carInfoParameter)
    {
        return Ok(await Mediator.Send(new FilterCarInfoQuery { CarInfoParameter = carInfoParameter }));
    }

    [HttpGet("car-info/{id}")]
    public async Task<ActionResult<Domain.Entities.CarInfo>> GetCarInfo(long id)
    {
        var query = _carInfoDbContext.CarInfos
            .Include(ci => ci.Images)
            .Include(ci => ci.Prices)
            .FirstOrDefaultAsync(ci => ci.Id == id);
        return Ok(await query);
    }
}