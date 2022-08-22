﻿using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Settings.Infrastructure
{
    public class SettingsContext : DbContext
    {
        #region DbSet properties

        public DbSet<OptionsList> Options { get; set; }
        public DbSet<OptionsListItem> OptionItems { get; set; }

        #endregion

        #region Constructors

        public SettingsContext(DbContextOptions<SettingsContext> options)
            : base(options) { }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new OptionsListConfiguration());
            builder.ApplyConfiguration(new OptionsListItemConfiguration());
        }
    }
}