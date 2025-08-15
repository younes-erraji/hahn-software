using FluentValidation;
using FluentValidation.Results;

using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Behaviours;

public class ValidationBehaviour<TRequest, IResponse> : IPipelineBehavior<TRequest, Response>
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
            ValidationResult[] validationResults = await Task.WhenAll(
                _validators.Select(X =>
                    X.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken))
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
