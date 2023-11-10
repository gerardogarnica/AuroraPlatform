using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Infrastructure.Seeds
{
    public class OptionsSeed : ISeedData<SettingsContext>
    {
        const string userBatch = "BATCH-USR";

        public void Seed(SettingsContext context)
        {
            var options = context.Options.ToList();

            if (!options.Any(x => x.Code.Equals("AttributeType")))
                context.Options.Add(CreateAttributeTypeOption());

            context.SaveChanges();
        }

        private static OptionsCatalog CreateAttributeTypeOption()
        {
            return new OptionsCatalog()
            {
                Code = "AttributeType",
                Name = "Attribute Types",
                Description = "Level or scope of attribute settings.",
                IsVisible = true,
                IsEditable = false,
                Items = new List<OptionsCatalogItem>
                {
                    new OptionsCatalogItem()
                    {
                        Code = "Security",
                        Description = "Global security attributes.",
                        IsEditable = false,
                        IsActive = true,
                        CreatedBy = userBatch,
                        CreatedDate = DateTime.UtcNow,
                        LastUpdatedBy = userBatch,
                        LastUpdatedDate = DateTime.UtcNow
                    }
                }
            };
        }
    }
}