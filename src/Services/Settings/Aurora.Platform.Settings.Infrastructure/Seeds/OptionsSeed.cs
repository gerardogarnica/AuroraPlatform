using Aurora.Framework.Repositories;
using Aurora.Framework.Serialization;
using Aurora.Framework.Utils;
using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Infrastructure.Seeds
{
    public class OptionsSeed : ISeedData<SettingsContext>
    {
        const string userBatch = "BATCH-USR";

        public void Seed(SettingsContext context)
        {
            string path = Path.Combine(AssemblyUtils.GetExecutingAssemblyLocation(), "Seeds", "Data", "options.json");

            var optionsList = JsonSerializer.DeserializeFromFile<List<OptionsCatalog>>(path);

            optionsList
                .Where(option => !context.Options.ToList().Any(x => x.Code.Equals(option.Code)))
                .ToList()
                .ForEach(option =>
                {
                    option.Items.ForEach(x =>
                    {
                        x.CreatedBy = userBatch;
                        x.CreatedDate = DateTime.UtcNow;
                        x.LastUpdatedBy = userBatch;
                        x.LastUpdatedDate = DateTime.UtcNow;
                    });

                    context.Options.Add(option);
                });

            context.SaveChanges();
        }
    }
}