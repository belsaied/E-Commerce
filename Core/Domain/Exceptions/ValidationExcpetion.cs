namespace Domain.Exceptions
{
    public sealed class ValidationExcpetion:Exception
    {
        public IEnumerable<string> Errors { get; set; } = [];
        public ValidationExcpetion(IEnumerable<string> errors):base("Validation Failed")
        {
            Errors = errors;
        }
    }
}
