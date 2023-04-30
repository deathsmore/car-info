using DVG.AP.Cms.CarInfo.Domain.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace DVG.AP.Cms.CarInfo.Persistence
{
    public class CommonDbContext : DbContext
    {
        public readonly ILogger _logger;


        public CommonDbContext(DbContextOptions<CommonDbContext> options, ILoggerFactory loggerFactory) :
            base(options)
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
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Url> Urls { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<District> Districts { get; set; }

        public virtual DbSet<SeoInfo> SeoInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
        


            modelBuilder.ApplyConfiguration(new SeoInfoConfig());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class SeoInfoConfig : IEntityTypeConfiguration<SeoInfo>
    {
        public void Configure(EntityTypeBuilder<SeoInfo> entity)
        {
            entity.ToTable("SEOInfos");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.BackLink)
                .HasMaxLength(255)
                .HasComment("Back link, used in Indo");

            entity.Property(e => e.MetaDescription).HasColumnType("character varying");

            entity.Property(e => e.MetaKeyword).HasColumnType("character varying");

            entity.Property(e => e.MetaTitle).HasMaxLength(500);

            entity.Property(e => e.ObjectType)
                .HasComment("Loại object, ứng với trường ObjectId, VD: Seo info của article, của tin rao...");

            entity.Property(e => e.OgDescription).HasMaxLength(500);

            entity.Property(e => e.OgImage).HasMaxLength(500);

            entity.Property(e => e.OgTitle).HasMaxLength(255);

            entity.Property(e => e.Seokeyword)
                .HasColumnName("SEOKeyword")
                .HasMaxLength(500)
                .HasComment("Keyword để search trong bài viết các keyword tương ứng, chuyển thành dạng anchor text");

            entity.Property(e => e.TitleLink)
                .HasMaxLength(500)
                .HasComment("Title backlink, used in Indo");

            entity.Property(e => e.TitleSeo)
                .HasMaxLength(256)
                .HasComment("thay thế metatitle khi trường này trống, used in Phil");
        }
    }
}