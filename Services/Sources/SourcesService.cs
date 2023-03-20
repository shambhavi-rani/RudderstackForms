using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RudderstackForms.Models;

namespace RudderstackForms.Services.Sources
{
    public class SourcesService: ISourcesService
    {
        private readonly IMongoCollection<Source> _sourcesCollection;

        public SourcesService(
            IOptions<RudderstackDatabaseSettings> rudderstackDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                rudderstackDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                rudderstackDatabaseSettings.Value.DatabaseName);

            _sourcesCollection = mongoDatabase.GetCollection<Source>(
                rudderstackDatabaseSettings.Value.SourcesCollectionName);
        }

        public async Task<List<Source>> GetAsync() =>
            await _sourcesCollection.Find(_ => true).ToListAsync();

        public async Task<List<Source>> GetByTypeAsync(string type) =>
            await _sourcesCollection.Find(x => x.Type == type).ToListAsync();

        public async Task<Source?> GetAsync(string id) =>
            await _sourcesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Source newSource) =>
            await _sourcesCollection.InsertOneAsync(newSource);

        public async Task UpdateAsync(string id, Source updatedSource) =>
            await _sourcesCollection.ReplaceOneAsync(x => x.Id == id, updatedSource);

        public async Task RemoveAsync(string id) =>
            await _sourcesCollection.DeleteOneAsync(x => x.Type == id);
    }
}
