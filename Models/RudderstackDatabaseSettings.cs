namespace RudderstackForms.Models
{
    public class RudderstackDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string FormTemplatesCollectionName { get; set; } = null!;

        public string SourcesCollectionName { get; set; } = null!;
    }
}
