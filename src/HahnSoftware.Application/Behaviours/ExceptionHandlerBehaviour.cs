using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Exceptions;

using MediatR;

namespace HahnSoftware.Application.Behaviours;

public class ExceptionHandlerBehaviour<TRequest, IResponse> : IPipelineBehavior<TRequest, Response>
    where TRequest : notnull
{
    public async Task<Response> Handle(TRequest request, RequestHandlerDelegate<Response> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            return TryHandleAsync(ex);
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
        
        return Response.Error(exception, "Something went wrong, Please try again!");
    }
}
