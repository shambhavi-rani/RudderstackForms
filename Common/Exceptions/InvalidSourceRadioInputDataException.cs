namespace RudderstackForms.Common.Exceptions
{
    public class InvalidSourceRadioInputDataException : Exception
    {
        public InvalidSourceRadioInputDataException()
        {
        }

        public InvalidSourceRadioInputDataException(string message)
            : base(message)
        {
        }

        public InvalidSourceRadioInputDataException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
