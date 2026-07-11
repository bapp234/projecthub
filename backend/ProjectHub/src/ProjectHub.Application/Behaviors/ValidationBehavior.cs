using FluentValidation;
using MediatR;

namespace ProjectHub.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
    {
        var validators = _validators as IValidator<TRequest>[] ?? _validators.ToArray();
        if (validators.Length == 0)
            return await next();

        if (validators.Length == 1)
        {
            var singleResult = await validators[0]
                .ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken);

            if (!singleResult.IsValid)
                throw new ValidationException(singleResult.Errors);

            return await next();
        }
        var results = await Task.WhenAll(
            validators.Select(v =>
                v.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken)));

        var failures = results.SelectMany(r => r.Errors).ToList();

        if (failures.Count != 0)
            throw new ValidationException(failures);

        return await next();
    }
}