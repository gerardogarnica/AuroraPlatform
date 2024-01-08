using Newtonsoft.Json;

namespace Aurora.Framework.Serialization
{
    public class JsonSerializer
    {
        public static T DeserializeFromFile<T>(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Serialize<T>(T entity)
        {
            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(entity, settings);
        }
    }
}