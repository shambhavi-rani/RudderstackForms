using System.Text.Json;

namespace RudderstackForms.Services.Sources
{
    public static class SourceHelper
    {
        public static Dictionary<string, object>? GetUserDataFromJson(Dictionary<string, JsonElement> userDataFromJson)
        {
            var processedUserData = new Dictionary<string, object>();

            foreach (var dataEntryFromJson in userDataFromJson)
            {
                var dataEntryValue = GetDataEntryFromJson(dataEntryFromJson.Value);
                processedUserData.Add(dataEntryFromJson.Key, dataEntryValue);
            }

            return processedUserData;
        }

        private static object GetDataEntryFromJson(JsonElement jsonElement)
        {
            if (IsJsonElementBoolean(jsonElement))
            {
                return jsonElement.GetBoolean();
            }
            else if (IsJsonElementString(jsonElement))
            {
                return GetStringFromJsonElement(jsonElement);
            }
            else
            {
                //TODO: create and throw custom exception instead of this ex
                throw new InvalidDataException();
            }
        }

        private static string GetStringFromJsonElement(JsonElement jsonElement)
        {
            var stringData = jsonElement.GetString();
            
            if (stringData == null)
            {
                //TODO: create and throw custom exception instead of this ex
                throw new InvalidDataException();
            }
            return stringData;
        }

        private static bool IsJsonElementBoolean(JsonElement jsonElement)
        {
            return jsonElement.ValueKind == JsonValueKind.True || jsonElement.ValueKind == JsonValueKind.False;
        }

        private static bool IsJsonElementString(JsonElement jsonElement)
        {
            return jsonElement.ValueKind == JsonValueKind.String;
        }
    }
}
