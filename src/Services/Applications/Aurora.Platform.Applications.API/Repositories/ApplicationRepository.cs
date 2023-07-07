using Aurora.Platform.Applications.API.Data;
using Aurora.Platform.Applications.API.Models;
using MongoDB.Driver;

namespace Aurora.Platform.Applications.API.Repositories
{
    public interface IApplicationRepository
    {
        Task<Application> GetByCodeAsync(string code);
        Task<IReadOnlyList<Application>> GetListAsync();
    }

    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IApplicationContext _context;

        public ApplicationRepository(IApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        async Task<Application> IApplicationRepository.GetByCodeAsync(string code)
        {
            var filter = Builders<Application>.Filter.Eq("Code", code);

            return await _context
                .Applications
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        async Task<IReadOnlyList<Application>> IApplicationRepository.GetListAsync()
        {
            return await _context
                .Applications
                .Find(x => true)
                .ToListAsync();
        }
    }
}