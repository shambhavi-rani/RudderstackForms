namespace RudderstackForms.Common.Exceptions
{
    public class SourceTypeNotFoundException : Exception
    {
        public SourceTypeNotFoundException()
        {
        }

        public SourceTypeNotFoundException(string message)
            : base(message)
        {
        }

        public SourceTypeNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
