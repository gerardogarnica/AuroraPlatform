using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Settings.Infrastructure
{
    public class SettingsContext : DbContext
    {
        #region DbSet properties

        public DbSet<AttributeSetting> AttributeSettings { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<OptionsCatalog> Options { get; set; }
        public DbSet<OptionsCatalogItem> OptionItems { get; set; }

        #endregion

        #region Constructors

        public SettingsContext(DbContextOptions<SettingsContext> options)
            : base(options) { }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AttributeSettingConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeValueConfiguration());
            modelBuilder.ApplyConfiguration(new OptionsCatalogConfiguration());
            modelBuilder.ApplyConfiguration(new OptionsCatalogItemConfiguration());
        }
    }
}