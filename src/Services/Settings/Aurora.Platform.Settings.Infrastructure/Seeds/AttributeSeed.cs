using Aurora.Framework.Repositories;
using Aurora.Framework.Serialization;
using Aurora.Framework.Utils;
using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Infrastructure.Seeds
{
    public class AttributeSeed : ISeedData<SettingsContext>
    {
        public void Seed(SettingsContext context)
        {
            string path = Path.Combine(AssemblyUtils.GetExecutingAssemblyLocation(), "Seeds", "Data", "attributes.json");

            var attributesList = JsonSerializer.DeserializeFromFile<List<AttributeSetting>>(path);

            attributesList
                .Where(attribute => !context.AttributeSettings.ToList().Any(x => x.Code.Equals(attribute.Code)))
                .ToList()
                .ForEach(attribute =>
                {
                    context.AttributeSettings.Add(attribute);
                });

            context.SaveChanges();
        }
    }
}