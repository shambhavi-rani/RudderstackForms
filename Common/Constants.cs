using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RudderstackForms.Common
{
    public static class Constants
    {
        public const int FormTemplateFieldsCount = 1000;

        public const int SourceTypeMaxLength = 200;

        public const string SourceTypesRoute = "SourceTypes";

        public static readonly IList<string> BooleanValueString = new ReadOnlyCollection<string>
        (new List<string> {
            "True",
            "true",
            "TRUE",
            "False",
            "false",
            "FALSE"
        });
    }
}
