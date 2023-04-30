using System.Data;
using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Persistence.Profiles;
using DVG.AutoPortal.EFCore6;
using Microsoft.EntityFrameworkCore;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories;

public class Repository<T>
    : RepositoryBase<T>, IRepository<T> where T : class
{
    protected readonly CarInfoDbContext CarInfoDbContext;
    protected readonly IDbConnection DbConnection;
    protected MapperConfiguration MapperConfiguration;

    public Repository(CarInfoDbContext dbContext) : base(dbContext)
    {
        this.CarInfoDbContext = dbContext;
        DbConnection = dbContext.Database.GetDbConnection();
        MapperConfiguration = CreateFromProfileConfig();
    }

    #region Private Method

    private static MapperConfiguration CreateFromProfileConfig()
    {
        return new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfiles>(); });
    }

    #endregion

    public async Task<int> AddRangeAsync(IEnumerable<T> entities)
    {
        await CarInfoDbContext.AddRangeAsync(entities).ConfigureAwait(false);
        return await CarInfoDbContext.SaveChangesAsync();
    }

    protected async Task<PagedList<TEntity>> GetPagedAsync<TEntity>(int pageNumber, int pageSize,
        IQueryable<TEntity> queryable) where TEntity : class
    {
        var totalRecord = await queryable.AsNoTracking().CountAsync();
        if (totalRecord <= 0)
        {
            return new PagedList<TEntity>();
        }

        var rowSkip = (pageNumber - 1) * pageSize;

        var collections = await queryable.AsNoTracking().Skip(rowSkip).Take(pageSize).ToListAsync();
        return new PagedList<TEntity>(pageNumber, pageSize, totalRecord, collections);
    }
}