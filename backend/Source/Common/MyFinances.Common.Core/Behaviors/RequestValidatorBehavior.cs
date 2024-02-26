using FluentValidation;
using MediatR;
using MyFinances.Common.Core.Results;

namespace MyFinances.Common.Core.Behaviors;

public class RequestValidatorBehavior<TRequest, TResponse>(IValidator<TRequest>? validator)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result, new()
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validator is null)
            return await next();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        var result = new TResponse();

        result.WithErrors(validationResult.Errors.Select(x => x.ErrorMessage));

        return result;
    }
}