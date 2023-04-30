using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AP.Cms.CarInfo.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transmission = DVG.AP.Cms.CarInfo.Domain.Entities.Transmission;

namespace DVG.AP.Cms.CarInfo.Persistence
{
    public class CarInfoDbContext : DbContext
    {
        public readonly ILogger _logger;


        public CarInfoDbContext(DbContextOptions<CarInfoDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _logger = loggerFactory.CreateLogger("DbCommand");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                            // .UseNpgsql(_connectionString)
                            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                            // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                            .LogTo(message => _logger.LogInformation(message), new[] { DbLoggerCategory.Database.Command.Name },
                                LogLevel.Information)


                .EnableSensitiveDataLogging();
        }
        public virtual DbSet<NewCarBoxDetail> NewCarBoxDetails { get; set; } = null!;

        public virtual DbSet<BodyType> BodyTypes { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CarBestSelling> CarBestSelling { get; set; }
        public virtual DbSet<CarColor> CarColors { get; set; }
        public virtual DbSet<CarImage> CarImages { get; set; }
        public virtual DbSet<NewCarArticle> NewCarArticles { get; set; }
        public virtual DbSet<NewCarBrand> NewCarBrands { get; set; }
        public virtual DbSet<NewCarModel> NewCarModels { get; set; }
        public virtual DbSet<NewCarVariant> NewCarVariants { get; set; }
        public virtual DbSet<CarInfoPropertyValue> CarInfoPropertyValues { get; set; }
        public virtual DbSet<NewCarBox> NewCarBoxes { get; set; }
        public virtual DbSet<Domain.Entities.CarInfo> CarInfos { get; set; }
        public virtual DbSet<CarPrice> CarPrices { get; set; }
        public virtual DbSet<CarProperty> CarProperties { get; set; }
        public virtual DbSet<CarPropertyComboBox> CarPropertyComboBoxes { get; set; }
        public virtual DbSet<CarPropertyComboboxOption> CarPropertyComboboxOptions { get; set; }
        public virtual DbSet<CarPropertyGroup> CarPropertyGroups { get; set; }
        public virtual DbSet<CurrencyUnit> CurrencyUnit { get; set; }
        public virtual DbSet<FuelType> FuelTypes { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<ContentDetail> NewCarDetails { get; set; }
        public virtual DbSet<NewCarPage> NewCarPages { get; set; } = null!;
        public virtual DbSet<Transmission> Transmissions { get; set; }
        public virtual DbSet<Variant> Variants { get; set; }
        public virtual DbSet<Segment> Segments { get; set; }
        public virtual DbSet<ModelPropertySummary> ModelPropertySummaries { get; set; }
        public virtual DbSet<ModelPropertyValue> ModelPropertyValues { get; set; }
        public virtual DbSet<NewCarSEOInfos> NewCarSEOInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.ApplyConfiguration(new BodyTypeConfig());
            modelBuilder.ApplyConfiguration(new BrandConfig());
            modelBuilder.ApplyConfiguration(new CarBestSellingConfig());
            modelBuilder.ApplyConfiguration(new CarColorConfig());
            modelBuilder.ApplyConfiguration(new CarImageConfig());
            modelBuilder.ApplyConfiguration(new CarInfoConfig());
            modelBuilder.ApplyConfiguration(new CarInfoPropertyValueConfig());
            modelBuilder.ApplyConfiguration(new CarPriceConfig());
            modelBuilder.ApplyConfiguration(new CarPropertyComboBoxConfig());
            modelBuilder.ApplyConfiguration(new CarPropertyComboboxOptionConfig());
            modelBuilder.ApplyConfiguration(new CarPriceConfig());
            modelBuilder.ApplyConfiguration(new CarPropertyGroupConfig());
            modelBuilder.ApplyConfiguration(new CurrencyUnitConfig());
            modelBuilder.ApplyConfiguration(new FuelTypeConfig());
            modelBuilder.ApplyConfiguration(new ModelConfig());
            modelBuilder.ApplyConfiguration(new NewCarArticleConfiguration());
            modelBuilder.ApplyConfiguration(new NewCarBrandConfiguration());
            modelBuilder.ApplyConfiguration(new NewCarModelConfiguration());
            modelBuilder.ApplyConfiguration(new NewCarVariantConfiguration());
            modelBuilder.ApplyConfiguration(new NewCarBoxDetailConfiguration());
            modelBuilder.ApplyConfiguration(new NewCarPageConfig());

            modelBuilder.ApplyConfiguration(new TransmissionConfig());
            modelBuilder.ApplyConfiguration(new VariantConfig());
            //modelBuilder.Entity<Variant>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.HasSequence("newcarcolors_id_seq").HasMax(2147483647);
            modelBuilder.HasSequence("newcardetails_id_seq").HasMax(2147483647);
            modelBuilder.HasSequence("newcarimages_id_seq").HasMax(2147483647);
            modelBuilder.HasSequence("Variants_Id_seq").HasMax(2147483647);


            base.OnModelCreating(modelBuilder);
        }
    }
}