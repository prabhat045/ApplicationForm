namespace ApplicationForm.CustomException
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }

}
