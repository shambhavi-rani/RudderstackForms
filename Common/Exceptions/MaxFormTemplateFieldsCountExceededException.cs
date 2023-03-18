namespace RudderstackForms.Common.Exceptions
{
    public class MaxFormTemplateFieldsCountExceededException : Exception
    {
        public MaxFormTemplateFieldsCountExceededException()
        {
        }

        public MaxFormTemplateFieldsCountExceededException(string message)
            : base(message)
        {
        }

        public MaxFormTemplateFieldsCountExceededException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
