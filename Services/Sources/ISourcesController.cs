using RudderstackForms.Models;

namespace RudderstackForms.Services.Sources
{
    public interface ISourcesService: IService<Source>
    {
        public Task<List<Source>> GetByTypeAsync(string type);
    }
}
