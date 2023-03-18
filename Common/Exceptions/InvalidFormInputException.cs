namespace RudderstackForms.Common.Exceptions
{
    public class InvalidFormInputException : Exception
    {
        public InvalidFormInputException()
        {
        }

        public InvalidFormInputException(string message)
            : base(message)
        {
        }

        public InvalidFormInputException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
