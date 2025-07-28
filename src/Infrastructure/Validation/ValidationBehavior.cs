using System.Text.Json;
using FluentValidation;
using MediatR;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Infrastructure.Validation;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next(cancellationToken);
        }

        var context = new ValidationContext<TRequest>(request);

        var errorsDictionary = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorsMessage) => new
                {
                    Key = propertyName,
                    Values = errorsMessage.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

        if (errorsDictionary.Any())
        {
            var formattedErrors = string.Join(", ", errorsDictionary.Select(e =>
                $"{e.Key}: {string.Join(", ", e.Value)}"));

            throw new ValidationException(formattedErrors);
        }

        return await next(cancellationToken);
    }
}