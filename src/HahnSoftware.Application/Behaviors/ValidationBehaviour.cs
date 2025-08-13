using FluentValidation;
using FluentValidation.Results;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace CleanArchitecture.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest> : IPipelineBehavior<TRequest, Response>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<Response> Handle(TRequest request, RequestHandlerDelegate<Response> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken))
            );

            List<ValidationFailure> failures = validationResults
                .Where(x => x.Errors.Any())
                .SelectMany(x => x.Errors)
                .ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        return await next();
    }
}
