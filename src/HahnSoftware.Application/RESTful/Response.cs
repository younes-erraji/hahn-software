using System.Net;
using System.Text.Json.Serialization;

namespace HahnSoftware.Application.RESTful;

public sealed class Response : IResponse
{
    public bool Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    public ushort StatusCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Exception { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, IEnumerable<string>>? Errors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public dynamic? Payload { get; set; }

    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Path { get; set; }

    public static Response Success()
    {
        return new Response()
        {
            Status = true,
            StatusCode = Get(HttpStatusCode.OK)
        };
    }

    public static Response Success(string message)
    {
        return new Response()
        {
            Message = message,
            Status = true,
            StatusCode = Get(HttpStatusCode.OK)
        };
    }

    public static Response Success<T>(T payload)
    {
        return new Response()
        {
            Status = true,
            Payload = payload,
            StatusCode = Get(HttpStatusCode.OK)
        };
    }

    public static Response Success<T>(string message, T payload)
    {
        return new Response()
        {
            Message = message,
            Status = true,
            Payload = payload,
            StatusCode = Get(HttpStatusCode.OK)
        };
    }
    
    public static Response Created<T>(string message, T payload)
    {
        return new Response()
        {
            Message = message,
            Status = true,
            Payload = payload,
            StatusCode = Get(HttpStatusCode.Created)
        };
    }

    public static Response Forbidden()
    {
        return new Response()
        {
            Message = "Accessing to this feature is restricted: Insufficient permissions!",
            Status = false,
            StatusCode = Get(HttpStatusCode.Forbidden)
        };
    }

    public static Response Forbidden(string message)
    {
        return new Response()
        {
            Message = message,
            Status = false,
            StatusCode = Get(HttpStatusCode.Forbidden)
        };
    }

    public static Response Unauthorized()
    {
        return new Response()
        {
            Message = "You are not authorized to access this resource. Please log in to continue.",
            Status = false,
            StatusCode = Get(HttpStatusCode.Unauthorized)
        };
    }

    public static Response Unauthorized(string message)
    {
        return new Response()
        {
            Message = message,
            Status = false,
            StatusCode = Get(HttpStatusCode.Unauthorized)
        };
    }

    public static Response NotFound()
    {
        return new Response()
        {
            Message = ResponseImmutable.NotFound(),
            Status = false,
            StatusCode = Get(HttpStatusCode.NotFound)
        };
    }

    public static Response NotFound(string type)
    {
        return new Response()
        {
            Message = ResponseImmutable.NotFound(type),
            Status = false,
            StatusCode = Get(HttpStatusCode.NotFound)
        };
    }

    public static Response Error(Dictionary<string, IEnumerable<string>> errors)
    {
        // Message = "The provided data is invalid. Please review and correct the errors below."
        // Message = "Please correct the highlighted fields and try again."
        // Message = "The request contains invalid data. Please check the error details and try again."
        // Message = "Unable to process your request due to validation errors. Please address all highlighted issues."
        return new Response()
        {
            Status = false,
            Message = "Unable to process your request due to validation errors.",
            StatusCode = Get(HttpStatusCode.BadRequest),
            Errors = errors
        };
    }

    public static Response Error(Exception exception)
    {
        return new Response()
        {
            Status = false,
            Exception = exception.Message,
            Message = ResponseImmutable.Error,
            StatusCode = Get(HttpStatusCode.BadRequest)
        };
    }

    public static Response Error(Exception exception, Dictionary<string, IEnumerable<string>> errors)
    {
        return new Response()
        {
            Exception = exception.Message,
            Message = ResponseImmutable.Error,
            Status = false,
            Errors = errors,
            StatusCode = Get(HttpStatusCode.BadRequest)
        };
    }

    public static Response Error(Exception exception, string message)
    {
        return new Response()
        {
            Exception = exception.Message,
            Message = message,
            Status = false,
            StatusCode = Get(HttpStatusCode.BadRequest)
        };
    }

    public static Response BadRequest()
    {
        return new Response()
        {
            Message = ResponseImmutable.Error,
            Status = false,
            StatusCode = Get(HttpStatusCode.BadRequest)
        };
    }

    public static Response BadRequest(string message)
    {
        return new Response()
        {
            Message = message,
            Status = false,
            StatusCode = Get(HttpStatusCode.BadRequest)
        };
    }

    private static ushort Get(HttpStatusCode statusCode)
    {
        return (ushort)statusCode;
    }
}
