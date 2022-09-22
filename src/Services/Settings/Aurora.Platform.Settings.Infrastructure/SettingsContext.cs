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
        public DbSet<OptionsList> Options { get; set; }
        public DbSet<OptionsListItem> OptionItems { get; set; }

        #endregion

        #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SettingsContext(DbContextOptions<SettingsContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(options) { }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AttributeSettingConfiguration());
            builder.ApplyConfiguration(new AttributeValueConfiguration());
            builder.ApplyConfiguration(new OptionsListConfiguration());
            builder.ApplyConfiguration(new OptionsListItemConfiguration());
        }
    }
}