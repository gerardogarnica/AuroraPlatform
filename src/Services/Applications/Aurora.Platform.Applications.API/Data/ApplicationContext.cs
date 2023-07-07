using Aurora.Platform.Applications.API.Models;
using MongoDB.Driver;

namespace Aurora.Platform.Applications.API.Data
{
    public class ApplicationContext : IApplicationContext
    {
        public IMongoCollection<Application> Applications { get; }

        public ApplicationContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDatabaseSettings:DatabaseName"));

            Applications = database.GetCollection<Application>("applications");
            ApplicationContextSeed.SeedData(Applications);
        }
    }
}