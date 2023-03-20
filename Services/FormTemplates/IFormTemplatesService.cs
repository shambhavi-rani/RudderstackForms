using RudderstackForms.Models;

namespace RudderstackForms.Services.FormTemplates
{
    public interface IFormTemplatesService: IService<FormTemplate>
    {
        public Task<List<string>> GetSourceTypesAsync();
    }
}
