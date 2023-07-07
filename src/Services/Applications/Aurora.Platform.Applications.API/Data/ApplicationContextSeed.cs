using Aurora.Platform.Applications.API.Models;
using MongoDB.Driver;

namespace Aurora.Platform.Applications.API.Data
{
    public class ApplicationContextSeed
    {
        public static void SeedData(IMongoCollection<Application> applicationCollection)
        {
            if (!applicationCollection.Find(x => true).Any())
            {
                applicationCollection.InsertManyAsync(GetPreconfiguredApplications());
            }
        }

        private static IEnumerable<Application> GetPreconfiguredApplications()
        {
            return new List<Application>()
            {
                new Application()
                {
                    Code = "DBB1F084-0E5C-488F-8990-EA1FDF223A94",
                    Name = "Aurora Platform",
                    Description = "Aurora Soft applications platform",
                    HasCustomConfig = false
                },
                new Application()
                {
                    Code = "C5F1BB99-D709-4AEB-8379-1424383B88F3",
                    Name = "Testing App",
                    Description = "Testing App",
                    HasCustomConfig = true
                }
            };
        }
    }
}