using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Helpers.OrderHelper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AP.Cms.CarInfo.Persistence.Repositories;
using DVG.AP.Cms.CarInfo.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DVG.AP.Cms.CarInfo.Persistence
{
    public static class CarInfoPersistenceServiceRegistration
    {
        public static IServiceCollection AddCarInfoPersistenceService(this IServiceCollection services,
            IConfiguration configuration)
        {
            // services.AddDbContext<NewCarsDbContext>(option =>
            //     option.UseNpgsql(connectionString, b => b.MigrationsAssembly("DVG.AutoPortal.CMS.NewCars.Api")));

            var connectionString = configuration.GetConnectionString("DBCarInfo");

            services.AddDbContext<CarInfoDbContext>(option =>
                option.UseNpgsql(connectionString));

            var connectionStringCommon = configuration.GetConnectionString("CommonDb");
            Console.WriteLine($"ConnectionString:{DateTime.Now}: {connectionStringCommon}");
            Console.WriteLine($"ConnectionString: {connectionString}");
            services.AddDbContext<CommonDbContext>(option =>
                option.UseNpgsql(connectionStringCommon));


            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(Repository<>));
            services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
            services.AddScoped<ICarInfoPropertyValueRepository, CarInfoPropertyValueRepository>();
            services.AddScoped<ICarInfoRepository, CarInfoRepository>();
            services.AddScoped<ICarColorRepository, CarColorRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IVariantRepository, VariantRepository>();
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<ISeoInfoRepository, SeoInfoRepository>();
            services.AddScoped(typeof(INewCarBoxDetailRepository), typeof(NewCarBoxDetailRepository));
            services.AddScoped<INewCarArticleRepository, NewCarArticleRepository>();
            services.AddScoped<INewCarBrandRepository, NewCarBrandRepository>();
            services.AddScoped<INewCarModelRepository, NewCarModelRepository>();
            services.AddScoped<INewCarVariantRepository, NewCarVariantRepository>();
            services.AddScoped<ISegmentRepository, SegmentRepository>();
            services.AddScoped<INewCarPageRepository, NewCarPageRepository>();
            services.AddScoped<IModelPropertyValueRepository, ModelPropertyValueRepository>();
            services.AddScoped<ICarPropertyRepository, CarPropertyRepository>();
            services.AddTransient<IModelPropertySummaryRepository, ModelPropertySummaryRepository>();
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            return services;
        }
    }
}
