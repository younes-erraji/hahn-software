namespace HahnSoftware.Domain.Exceptions;

public class ValidationException : Exception
{
    public Dictionary<string, IEnumerable<string>> Errors { get; set; }

    public ValidationException(Dictionary<string, IEnumerable<string>> errors) : base("Unable to process your request due to validation errors.")
    {
        Errors = errors;
    }
}
