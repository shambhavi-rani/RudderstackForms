namespace RudderstackForms.Common.Exceptions
{
    public class SourceUserDataMissingRequiredFormFields : Exception
    {
        public SourceUserDataMissingRequiredFormFields()
        {
        }

        public SourceUserDataMissingRequiredFormFields(string message)
            : base(message)
        {
        }

        public SourceUserDataMissingRequiredFormFields(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
