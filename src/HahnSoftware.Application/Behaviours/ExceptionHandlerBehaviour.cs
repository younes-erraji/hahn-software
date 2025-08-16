using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Exceptions;

using MediatR;

namespace HahnSoftware.Application.Behaviours;

public class ExceptionHandlerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull where TResponse : notnull, IResponse
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            return (TResponse)(IResponse)TryHandleAsync(ex);
        }
    }

    public Response TryHandleAsync(Exception exception)
    {
        if (exception is NotFoundException notFoundException)
        {
            if (string.IsNullOrWhiteSpace(notFoundException.Type))
                return Response.NotFound(notFoundException.Type);
            else
                return Response.NotFound();
        }
        else if (exception is ValidationException validatinException)
        {
            return Response.Error(validatinException.Errors);
        }

        return Response.Error(exception, "Something went wrong, Please try again!");
    }
}
