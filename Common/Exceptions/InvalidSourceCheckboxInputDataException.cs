namespace RudderstackForms.Common.Exceptions
{
    public class InvalidSourceCheckboxInputDataException : Exception
    {
        public InvalidSourceCheckboxInputDataException()
        {
        }

        public InvalidSourceCheckboxInputDataException(string message)
            : base(message)
        {
        }

        public InvalidSourceCheckboxInputDataException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
