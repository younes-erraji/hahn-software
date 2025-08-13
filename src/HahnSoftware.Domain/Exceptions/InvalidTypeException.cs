namespace HahnSoftware.Domain.Exceptions;

public class InvalidTypeException : Exception
{
    public InvalidTypeException() : base()
    {
    }

    public InvalidTypeException(string message) : base(message)
    {
    }
}
