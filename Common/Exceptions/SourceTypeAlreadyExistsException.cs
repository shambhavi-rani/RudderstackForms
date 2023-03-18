namespace RudderstackForms.Common.Exceptions
{
    public class SourceTypeAlreadyExistsException: Exception
    {
        public SourceTypeAlreadyExistsException()
        {
        }

        public SourceTypeAlreadyExistsException(string message)
            : base(message)
        {
        }

        public SourceTypeAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
