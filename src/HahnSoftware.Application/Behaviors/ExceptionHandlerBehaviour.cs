using HahnSoftware.Application.RESTful;

using MediatR;

namespace CleanArchitecture.Application.Common.Behaviours;

public class ExceptionHandlerBehaviour<TRequest> : IPipelineBehavior<TRequest, Response>
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
            return Response.Error(ex);
        }
    }
}
