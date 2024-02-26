using MediatR;
using MyFinances.Common.Core.Cqrs;
using MyFinances.Common.Core.UnitOfWork;

namespace MyFinances.Common.Core.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            return await next();
        }
        catch (Exception)
        {
            await unitOfWork.Rollback();
            throw;
        }
    }
}