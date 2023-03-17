using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RudderstackForms.Models;

namespace RudderstackForms.Services.FormTemplates
{
    public class FormTemplatesService
    {
        private readonly IMongoCollection<FormTemplate> _formTemplatesCollection;

        public FormTemplatesService(
            IOptions<RudderstackDatabaseSettings> rudderstackDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                rudderstackDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                rudderstackDatabaseSettings.Value.DatabaseName);

            _formTemplatesCollection = mongoDatabase.GetCollection<FormTemplate>(
                rudderstackDatabaseSettings.Value.FormTemplatesCollectionName);
        }

        public async Task<List<FormTemplate>> GetAsync() =>
            await _formTemplatesCollection.Find(_ => true).ToListAsync();

        public async Task<FormTemplate?> GetAsync(string sourceType) =>
            await _formTemplatesCollection.Find(x => x.Type == sourceType).FirstOrDefaultAsync();

        public async Task CreateAsync(FormTemplate newFormTemplate) =>
            await _formTemplatesCollection.InsertOneAsync(newFormTemplate);

        public async Task UpdateAsync(string sourceType, FormTemplate updatedFormTemplate) =>
            await _formTemplatesCollection.ReplaceOneAsync(x => x.Type == sourceType, updatedFormTemplate);

        public async Task RemoveAsync(string sourceType) =>
            await _formTemplatesCollection.DeleteOneAsync(x => x.Type == sourceType);
    }
}
