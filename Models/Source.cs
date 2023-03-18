using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RudderstackForms.Services.Sources;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace RudderstackForms.Models
{
    public class Source
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public Dictionary<string, string>? UserData { get; set; }

        /*public Source(string type, Dictionary<string, JsonElement> userDataFromJson)
        {
            Type = type;
            UserData = SourceHelper.GetUserDataFromJson(userDataFromJson);
        }*/
    }
}
