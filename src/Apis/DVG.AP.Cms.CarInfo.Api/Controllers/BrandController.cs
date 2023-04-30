using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Brand.Queries.GetAllByConditions;
using DVG.AP.Cms.CarInfo.Application.Features.Brand.Queries.GetAllByConditions.Models;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVG.AP.Cms.CarInfo.Api.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/v{version:apiVersion}/brands")]
public class BrandController : ApiControllerBase
{
    private readonly IRepository<Brand> _brandRepository;
    private readonly CarInfoDbContext _carInfoDbContext;

    private readonly ILogger<BrandController> _logger;
    // GET

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public BrandController(IMediator mediator, IRepository<Brand> brandRepository, ILogger<BrandController> logger,
        CarInfoDbContext carInfoDbContext) :
        base(mediator)
    {
        _brandRepository = brandRepository;
        _logger = logger;
        _carInfoDbContext = carInfoDbContext;
    }

    /// <summary>
    /// Get list brand by status
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BrandVm>>> GetBrandByStatus([FromQuery] GetAllQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}