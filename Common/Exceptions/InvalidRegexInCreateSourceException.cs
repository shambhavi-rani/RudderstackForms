namespace RudderstackForms.Common.Exceptions
{
    public class InvalidRegexInCreateSourceException : Exception
    {
        public InvalidRegexInCreateSourceException()
        {
        }

        public InvalidRegexInCreateSourceException(string message)
            : base(message)
        {
        }

        public InvalidRegexInCreateSourceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
