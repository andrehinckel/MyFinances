using MediatR;
using MyFinances.Common.Core.Results;

namespace MyFinances.Common.Core.Cqrs;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull;