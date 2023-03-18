using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RudderstackForms.Models;

namespace RudderstackForms.Services
{
    public interface IService<T>
    {
        public Task<List<T>> GetAsync();

        public Task<T?> GetAsync(string id);

        public Task CreateAsync(T newObject);

        public Task UpdateAsync(string id, T updateObject);

        public Task RemoveAsync(string id);
    }
}
