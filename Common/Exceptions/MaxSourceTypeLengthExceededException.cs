namespace RudderstackForms.Common.Exceptions
{
    public class MaxSourceTypeLengthExceededException : Exception
    {
        public MaxSourceTypeLengthExceededException()
        {
        }

        public MaxSourceTypeLengthExceededException(string message)
            : base(message)
        {
        }

        public MaxSourceTypeLengthExceededException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
