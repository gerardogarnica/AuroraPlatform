using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Infrastructure.Seed
{
    public class SettingsContextSeed
    {
        public static async Task SeedAsync(SettingsContext context)
        {
            if (!context.Options.Any())
            {
                context.Options.AddRange(GetGlobalOptionsList());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<OptionsList> GetGlobalOptionsList()
        {
            return new List<OptionsList>
            {
                new OptionsList()
                {
                    Code = "AttributeType",
                    Name = "Attribute Types",
                    Description = "Level or scope of attribute settings.",
                    IsVisible = true,
                    IsEditable = false,
                    Items = new List<OptionsListItem>
                    {
                        new OptionsListItem()
                        {
                            Code = "Security",
                            Description = "Global security attributes.",
                            IsEditable = false,
                            IsActive = true,
                            CreatedBy = "BATCH-USR",
                            CreatedDate = DateTime.UtcNow,
                            LastUpdatedBy = "BATCH-USR",
                            LastUpdatedDate = DateTime.UtcNow
                        }
                    }
                }
            };
        }
    }
}