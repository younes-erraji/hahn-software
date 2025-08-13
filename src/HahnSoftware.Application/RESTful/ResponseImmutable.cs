namespace HahnSoftware.Application.RESTful;

public sealed class ResponseImmutable
{
    public static string NotFound(string type)
    {
        return $"The system does not recognize the specified {type}!";
    }

    public static string NotFound()
    {
        return $"The system does not recognize the specified record!";
    }

    public static readonly string Error = "An unexpected error occurred. Please try again later.";
}
