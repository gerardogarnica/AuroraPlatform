using Aurora.Platform.Applications.API.Models;
using MongoDB.Driver;

namespace Aurora.Platform.Applications.API.Data
{
    public interface IApplicationContext
    {
        IMongoCollection<Application> Applications { get; }
    }
}