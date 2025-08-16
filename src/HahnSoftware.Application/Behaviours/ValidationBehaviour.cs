using FluentValidation;
using FluentValidation.Results;

using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Exceptions;

using MediatR;

namespace HahnSoftware.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull where TResponse : notnull, IResponse
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
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
            {
                Dictionary<string, IEnumerable<string>> errors = failures
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).AsEnumerable()
                    );

                throw new Domain.Exceptions.ValidationException(errors);
            }
        }

        return await next();
    }
}
