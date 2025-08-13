namespace HahnSoftware.Domain.Exceptions;

public class NotFoundException : Exception
{
    public string Type { get; set; }

    public NotFoundException() : base($"The system does not recognize the specified entity!")
    {
        Type = "entity";
    }

    public NotFoundException(string type) : base($"The system does not recognize the specified {type}!")
    {
        Type = type;
    }
}
