using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RudderstackForms.Models
{
    public static class Constants
    {
        public const int FormTemplateFieldsCount = 1000;

        public const int SourceTypeMaxLength = 200;

        public const string SourceTypesRoute = "SourceTypes";

        public static readonly IList<String> BooleanValueString = new ReadOnlyCollection<string>
        (new List<String> {
            "True",
            "true",
            "TRUE",
            "False",
            "false",
            "FALSE"
        });
    }
}
