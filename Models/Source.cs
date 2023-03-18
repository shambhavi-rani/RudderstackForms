using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RudderstackForms.Models
{
    public class Source
    {
        [BsonId]
        public string Id { get; set; } = null!;

        [Required]
        public string Type { get; set; }

        [Required]
        public Dictionary<string, object> UserData { get; set; }

        public Source(string type, Dictionary<string, Object> userData)
        {
            Type = type;
            UserData = userData;
        }
    }
}
